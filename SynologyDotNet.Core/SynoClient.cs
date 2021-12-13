using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Model;
using SynologyDotNet.Core.Responses;
using SynologyDotNet.Core.Exceptions;

namespace SynologyDotNet
{
    /// <summary>
    /// Re-usable client to initialize a session. Can be used by all connectors.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class SynoClient : IDisposable
    {
        #region Fields

        /// <summary>
        /// A session name must be provided to connect. By default, the connector will use the "DSM" name to cover all permissions.
        /// </summary>
        private const string SynoTokenHeader = "X-SYNO-TOKEN";
        private object _apiInfosLock = new object();

        #endregion

        #region Apis

        private const string SYNO_Auth = "SYNO.API.Auth";
        private const string SYNO_Info = "SYNO.API.Info";
        private const string SYNO_Core_Desktop_Initdata = "SYNO.Core.Desktop.Initdata";
        private const string SYNO_Core_Desktop_SessionData = "SYNO.Core.Desktop.SessionData";
        private const string SYNO_FileStation_Info = "SYNO.FileStation.Info";

        private static readonly HashSet<string> _implementedApiNames = new HashSet<string>(new[]
        {
            SYNO_Info,
            SYNO_Auth,
            SYNO_Core_Desktop_Initdata,
            SYNO_Core_Desktop_SessionData,
            SYNO_FileStation_Info
        });

        #endregion

        #region Properties

        private readonly Dictionary<string, ApiInfo> _apiInfos = new Dictionary<string, ApiInfo>();
        /// <summary>
        /// Gets the API infos.
        /// </summary>
        /// <value>
        /// The API infos.
        /// </value>
        /// <exception cref="System.InvalidOperationException">Please call {nameof(ConnectAsync)} first!</exception>
        protected Dictionary<string, ApiInfo> ApiInfos
        {
            get
            {
                if (_apiInfos.Count == 0)
                    throw new InvalidOperationException($"Please call {nameof(ConnectAsync)} first!");
                return _apiInfos;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is conneted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is conneted; otherwise, <c>false</c>.
        /// </value>
        public bool IsConneted => _apiInfos.Count > 0;

        /// <summary>
        /// Gets the user session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public SynoSession Session { get; private set; }

        private readonly HttpClient _httpClient;
        /// <summary>
        /// Gets the underlying HttpClient.
        /// </summary>
        /// <value>
        /// The HTTP client.
        /// </value>
        public HttpClient HttpClient => _httpClient;

        private readonly List<StationConnectorBase> _connectors = new List<StationConnectorBase>();
        /// <summary>
        /// Gets the connectors added to this client. 
        /// These connectors are attached to this SynoClient, so they send requests through this instance.
        /// </summary>
        public StationConnectorBase[] Connectors => _connectors.ToArray();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SynoClient"/> class.
        /// </summary>
        /// <param name="server">The URI of the Synology server. Make sure it also contains the correct port number (By default it is 5000/5001 for HTTP/HTTPS)</param>
        /// <param name="connectors"></param>
        public SynoClient(Uri server, params StationConnectorBase[] connectors)
            : this(server, false, connectors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynoClient"/> class.
        /// </summary>
        /// <param name="server">The URI of the Synology server. Make sure it also contains the correct port number (By default it is 5000/5001 for HTTP/HTTPS)</param>
        public SynoClient(Uri server)
            : this(server, false, new StationConnectorBase[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynoClient"/> class.
        /// </summary>
        /// <param name="server">The URI of the Synology server. Make sure it also contains the correct port number (By default it is 5000/5001 for HTTP/HTTPS)</param>
        /// <param name="bypassSslCertificateValidation">if set to <c>true</c> [bypass SSL certificate validation].</param>
        public SynoClient(Uri server, bool bypassSslCertificateValidation)
            : this(server, bypassSslCertificateValidation, new StationConnectorBase[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynoClient"/> class.
        /// </summary>
        /// <param name="server">The URI of the Synology server. Make sure it also contains the correct port number (By default it is 5000/5001 for HTTP/HTTPS)</param>
        /// <param name="bypassSslCertificateValidation">if set to <c>true</c> [bypass SSL certificate validation].</param>
        /// <param name="connectors">Adds the specified connectors to this client, so they will send requests with this client.</param>
        public SynoClient(Uri server, bool bypassSslCertificateValidation, params StationConnectorBase[] connectors)
        {
            _httpClient = HttpClientHelper.CreateHttpClient(server, System.Security.Authentication.SslProtocols.Tls12, bypassSslCertificateValidation);
            if (connectors?.Length > 0 == true)
            {
                _connectors.AddRange(connectors);
                UpdateApiNamesFromConnectors(connectors);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified connector to this client.
        /// </summary>
        /// <param name="connector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SynoClient Add(StationConnectorBase connector)
        {
            if (connector is null)
                throw new ArgumentNullException(nameof(connector));
            if (_connectors.Contains(connector))
                throw new ArgumentException("This connector has been added already.", nameof(connector));
            if (IsConneted)
                throw new InvalidOperationException($"This client has been connected already. You can add connectors synchronously only before calling login. After login has been called, please use {nameof(AddAsync)}");

            UpdateApiNamesFromConnector(connector);
            _connectors.Add(connector);
            return this;
        }

        /// <summary>
        /// Adds the specified connector to this client and loads the API specifications used by this client.
        /// This method can be called even after this SynoClient is connected, in this case the API specification for this connector will be downloaded.
        /// </summary>
        /// <param name="connector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(StationConnectorBase connector)
        {
            if (connector is null)
                throw new ArgumentNullException(nameof(connector));
            if (_connectors.Contains(connector))
                throw new ArgumentException("This connector has been added already.", nameof(connector));

            if (IsConneted)
                await LoadApiInfos(((ISynoClientConnectable)connector).GetApiNames());
            else
                UpdateApiNamesFromConnector(connector);
            _connectors.Add(connector);
        }

        /// <summary>
        /// Loads specification for the specified API names into this client.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task LoadApiInfos(string[] filter = null)
        {
            var apis = await QueryApiInfos(filter);
            lock (_apiInfosLock)
            {
                foreach (var api in apis)
                    _apiInfos[api.Name] = api;
            }
        }

        /// <summary>
        /// Gets the supported API specifications from the Synology DSM.
        /// </summary>
        /// <returns>List of API specifications.</returns>
        /// <exception cref="System.Exception">Cannot get Synology API information.</exception>
        public async Task<ApiInfo[]> QueryApiInfos() => await QueryApiInfos(null);

        /// <summary>
        /// Gets the supported API specifications from the Synology DSM filtered down to the specified APIs.
        /// </summary>
        /// <param name="filter">Filter the list by API names. Get API specifications only for these API names.</param>
        /// <returns>List of API specifications.</returns>
        /// <exception cref="System.Exception">Cannot get Synology API information.</exception>
        public async Task<ApiInfo[]> QueryApiInfos(string[] filter)
        {
            var req = new RequestBuilder("webapi/query.cgi", SYNO_Info, 1, "query").SetParam("query", filter?.Length > 0 ? string.Join(",", filter) : "all");
            var json = await QueryStringAsync(req);
            var result = JsonConvert.DeserializeObject<SynoApiInfo>(json);
            if (!result.Success || !(result.Data?.Count > 0))
                throw new Exception("Cannot get Synology API information.");
            return result.Data.Select(x => new ApiInfo(x.Key, x.Value)).ToArray();
        }

        /// <summary>
        /// Login with username and password.
        /// </summary>
        /// <param name="username">Login account name.</param>
        /// <param name="password">Login account password.</param>
        /// <param name="session">Optional. Session name for DSM applications.</param>
        /// <returns>Returns a user session. You can persist this information and re-use it next time. Instead of the login method, invoke RestoreSession so you can re-use your last session.</returns>
        public async Task<SynoSession> LoginAsync(string username, string password, string session = "")
        {
            await ConnectAsync();

            var req = new RequestBuilder(GetApiInfo(SYNO_Auth)).Method("login");
            req["account"] = username;
            req["passwd"] = password;
            req["session"] = session;
            req["format"] = "cookie";
            req["enable_syno_token"] = "yes";
            req.SetExplicitQueryStringParam("enable_syno_token", "yes"); // This is necessary to get a valid synotoken, this has to be present in the query string as well (even if it's a POST!)

            var response = await _httpClient.SendAsync(req.ToPostRequest());
            ThrowIfNotSuccessfulHttpResponse(response);
            var json = Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync());
            var loginResult = JsonConvert.DeserializeObject<LoginResult>(json);

            if (!(loginResult is null))
            {
                if (loginResult.Success && response.Headers.TryGetValues("Set-Cookie", out var cookies))
                {
                    var result = new SynoSession()
                    {
                        Name = session,
                        Id = loginResult.Data.SID,
                        Token = loginResult.Data.SynoToken,
                        Cookie = cookies.ToArray()
                    };
                    LoadSession(result);
                    return result;
                }
                throw new SynoLoginException(loginResult.Error.Code);
            }
            throw new SynoLoginException(0);
        }

        /// <summary>
        /// Re-uses previous session.
        /// </summary>
        /// <param name="previousSession">Session</param>
        /// <param name="sendTestRequest">Set to true in order to test the credentials with a test request. If you make a request anyway after this call, it is better to set it to False.</param>
        /// <returns></returns>
        /// <exception cref="SynoLoginException">Thrown if sendTestRequest was True and the login test failed.</exception>
        public async Task LoginWithPreviousSessionAsync(SynoSession previousSession, bool sendTestRequest = true)
        {
            await ConnectAsync();
            LoadSession(previousSession);
            if (sendTestRequest)
            {
                var loginTest = await QueryObjectAsync<ApiResponse>(SYNO_FileStation_Info, "get");
                if (!loginTest.Success)
                    throw new SynoLoginException(loginTest.Error.Code);
            }
        }

        /// <summary>
        /// Cancels the current session.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LogoutAsync()
        {
            if (!(Session is null))
            {
                var req = new RequestBuilder(GetApiInfo(SYNO_Auth)).Method("logout");
                var logoutResult = await QueryObjectAsync<ApiResponse>(req);
                req["session"] = Session.Name;
                ClearSession();
                return logoutResult.Success;
            }
            return false;
        }

        /// <summary>
        /// Gets the external ip and hostname. Works only, if the related settings are configured correctly on the Synology server.
        /// </summary>
        /// <returns></returns>
        public async Task<ApiDataResponse<ExternalIpData>> GetExternalIpAndHostname()
        {
            var req = new RequestBuilder(GetApiInfo(SYNO_Core_Desktop_Initdata)).Action("external_ip").Method("get");
            var result = await QueryObjectAsync<ApiDataResponse<ExternalIpData>>(req);
            return result;
        }

        /// <summary>
        /// Gets the API information for the specified API.
        /// </summary>
        /// <param name="apiName">Name of the API.</param>
        /// <returns>API info.</returns>
        /// <exception cref="System.NotSupportedException">The '{apiName}' API is not supported by your server.</exception>
        public ApiInfo GetApiInfo(string apiName)
        {
            if (ApiInfos.TryGetValue(apiName, out var api))
                return api;
            throw new NotSupportedException($"The '{apiName}' API is not supported by your server.");
        }

        /// <summary>
        /// Queries a list of entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiName">Name of the API.</param>
        /// <param name="method">The method.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<T> QueryListAsync<T>(string apiName, string method, int limit, int offset, params (string, object)[] parameters)
            where T : IApiListResponse
        {
            var req = new RequestBuilder(GetApiInfo(apiName)).Method(method).Limit(limit).Offset(offset).SetParams(parameters);
            var result = await QueryObjectAsync<T>(req);
            return result;
        }

        /// <summary>
        /// Queries an entity from the specified endpoint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiName">Name of the API.</param>
        /// <param name="method">The method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<T> QueryObjectAsync<T>(string apiName, string method, params (string, object)[] parameters)
        {
            var req = new RequestBuilder(GetApiInfo(apiName)).Method(method).SetParams(parameters);
            var result = await QueryObjectAsync<T>(req);
            return result;
        }

        /// <summary>
        /// Queries image data from the specified endpoint.
        /// </summary>
        /// <param name="apiName">Name of the API.</param>
        /// <param name="method">The method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="SynologyDotNet.Core.Exceptions.SynoHttpException"></exception>
        public async Task<ByteArrayData> QueryByteArrayAsync(string apiName, string method, params (string, object)[] parameters)
        {
            var req = new RequestBuilder(GetApiInfo(apiName)).Method(method).SetParams(parameters);
            return await QueryByteArrayAsync(req);
        }

        /// <summary>
        /// Sends a query and returns the response as a raw string.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns></returns>
        public async Task<string> QueryStringAsync(RequestBuilder req)
        {
            var response = await _httpClient.SendAsync(req.ToPostRequest());
            ThrowIfNotSuccessfulHttpResponse(response);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var text = Encoding.UTF8.GetString(bytes);
            return text;
        }

        /// <summary>
        /// Sends a query and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req">The req.</param>
        /// <returns></returns>
        public async Task<T> QueryObjectAsync<T>(RequestBuilder req)
        {
            var response = await _httpClient.SendAsync(req.ToPostRequest());
            ThrowIfNotSuccessfulHttpResponse(response);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var text = Encoding.UTF8.GetString(bytes);
            var obj = JsonConvert.DeserializeObject<T>(text);
            return obj;
        }

        /// <summary>
        /// Queries the byte array asynchronous.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns></returns>
        /// <exception cref="SynologyDotNet.Core.Exceptions.SynoHttpException"></exception>
        public async Task<ByteArrayData> QueryByteArrayAsync(RequestBuilder req)
        {
            var response = await _httpClient.SendAsync(req.ToPostRequest());
            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadAsByteArrayAsync();
                return new ByteArrayData()
                {
                    Data = bytes,
                    Type = response.Content.Headers.ContentType.MediaType
                };
            }
            throw new SynoHttpException(response);
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
            _httpClient.Dispose();
        }

        private static void ThrowIfNotSuccessfulHttpResponse(HttpResponseMessage msg)
        {
            if (!msg.IsSuccessStatusCode)
                throw new SynoHttpException(msg);
        }

        #endregion

        #region Private Methods

        private async Task ConnectAsync()
        {
            if (IsConneted)
                throw new InvalidOperationException("Already connected.");
            await LoadApiInfos(_implementedApiNames.ToArray());
        }

        /// <summary>
        /// You can re-use the previous session, so you can skip the login.
        /// There is a possibility that your session already expired! In this case, you have to call the LoginAsync method and authenticate the user.
        /// </summary>
        /// <param name="session"></param>
        private void LoadSession(SynoSession session)
        {
            if (session is null)
                throw new ArgumentNullException(nameof(session));
            if (string.IsNullOrWhiteSpace(session.Id))
                throw new ArgumentException($"{nameof(session)}.{nameof(session.Id)}");
            if (string.IsNullOrWhiteSpace(session.Token) || session.Token.All(c => c == '-'))
                throw new ArgumentException($"{nameof(session)}.{nameof(session.Token)}");
            if (!(session.Cookie?.Length > 0))
                throw new ArgumentException($"{nameof(session)}.{nameof(session.Cookie)}");

            HttpClientHelper.SetDefaultRequestHeaderValue(_httpClient, SynoTokenHeader, session.Token);
            HttpClientHelper.SetDefaultRequestHeaderValues(_httpClient, "Cookie", session.Cookie);
            Session = session;
        }

        private void ClearSession()
        {
            Session = null;
            HttpClientHelper.SetDefaultRequestHeaderValue(_httpClient, SynoTokenHeader, null);
            HttpClientHelper.SetDefaultRequestHeaderValues(_httpClient, "Cookie", null);
        }

        private void UpdateApiNamesFromConnectors(IEnumerable<ISynoClientConnectable> connectors)
        {
            if (connectors is null)
                return;
            foreach (var connector in connectors)
                UpdateApiNamesFromConnector(connector);
        }

        private void UpdateApiNamesFromConnector(ISynoClientConnectable connector)
        {
            connector.SetClient(this);
            foreach (var apiName in connector.GetApiNames())
                _implementedApiNames.Add(apiName);
        }

        #endregion
    }
}
