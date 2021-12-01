﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Helpers
{
    /// <summary>
    /// Constructs Synology API requests
    /// </summary>
    public class RequestBuilder
    {
        #region Properties
        /// <summary>
        /// The CGI endpoint to call
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Request parameters.
        /// </summary>
        public Dictionary<string, string> Params { get; } = new Dictionary<string, string>();

        /// <summary>
        /// The parameters in this collection will be always serialized into the query string regardless of the request type
        /// </summary>
        public Dictionary<string, string> ExplicitQueryStringParams { get; } = new Dictionary<string, string>();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBuilder"/> class.
        /// </summary>
        public RequestBuilder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBuilder"/> class.
        /// </summary>
        /// <param name="apiInfo">The API information.</param>
        public RequestBuilder(ApiInfo apiInfo)
        {
            ApiInfo(apiInfo, string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBuilder"/> class.
        /// </summary>
        /// <param name="apiInfo">The API information.</param>
        /// <param name="subendpoint">The subendpoint.</param>
        public RequestBuilder(ApiInfo apiInfo, string subendpoint)
        {
            ApiInfo(apiInfo, subendpoint);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBuilder"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="api">The API.</param>
        /// <param name="version">The version.</param>
        /// <param name="method">The method.</param>
        public RequestBuilder(string endpoint, string api, int version, string method)
        {
            Endpoint = endpoint;
            Api(api);
            Version(version);
            Method(method);
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string this[string key]
        {
            get => Params[key];
            set => Params[key] = value;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        public RequestBuilder SetEndpoint(string endpoint)
        {
            Endpoint = endpoint;
            return this;
        }

        /// <summary>
        /// APIs the information.
        /// </summary>
        /// <param name="apiInfo">The API information.</param>
        /// <param name="subendpoint">The subendpoint.</param>
        /// <returns></returns>
        public RequestBuilder ApiInfo(ApiInfo apiInfo, string subendpoint = "")
        {
            Endpoint = $"webapi/{apiInfo.Path}{subendpoint}";
            Api(apiInfo.Name);
            Version(apiInfo.MaxVersion);
            return this;
        }

        /// <summary>
        /// APIs the specified API.
        /// </summary>
        /// <param name="api">The API.</param>
        /// <returns></returns>
        public RequestBuilder Api(string api)
        {
            SetParam("api", api);
            return this;
        }

        /// <summary>
        /// Versions the specified version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public RequestBuilder Version(int version)
        {
            SetParam("version", version.ToString());
            return this;
        }

        /// <summary>
        /// Methods the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public RequestBuilder Method(string method)
        {
            SetParam("method", method);
            return this;
        }

        /// <summary>
        /// Actions the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public RequestBuilder Action(string action)
        {
            SetParam("action", action);
            return this;
        }

        /// <summary>
        /// Limits the number of entries returned by the server. Use it in combination with Offset to impement pagination.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public RequestBuilder Limit(int limit)
        {
            SetParam("limit", limit.ToString());
            return this;
        }

        /// <summary>
        /// Offset from the beginning of the data array. Use it in combination with Limit to impement pagination.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public RequestBuilder Offset(int offset)
        {
            SetParam("offset", offset.ToString());
            return this;
        }

        /// <summary>
        /// Sets the parameter.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public RequestBuilder SetParam(string key, object value)
        {
            Params[key] = value?.ToString();
            return this;
        }

        /// <summary>
        /// Sets the parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public RequestBuilder SetParams(params (string, object)[] parameters)
        {
            foreach (var p in parameters)
                Params[p.Item1] = p.Item2?.ToString();
            return this;
        }

        /// <summary>
        /// Explicit query string parameters are always appended to the final URL, regardless the chosen HTTP method.
        /// </summary>
        public RequestBuilder SetExplicitQueryStringParam(string key, object value)
        {
            ExplicitQueryStringParams[key] = value?.ToString();
            return this;
        }

        /// <summary>
        /// Explicit query string parameters are always appended to the final URL, regardless the chosen HTTP method.
        /// </summary>
        public RequestBuilder SetExplicitQueryStringParams(params (string, object)[] parameters)
        {
            foreach (var p in parameters)
                ExplicitQueryStringParams[p.Item1] = p.Item2?.ToString();
            return this;
        }

        /// <summary>
        /// Converts to HTTP POST request.
        /// </summary>
        /// <returns></returns>
        public HttpRequestMessage ToPostRequest()
        {
            // Only ExplicitQueryStringParams are going to the query string
            string url = GetBaseUrl();
            if (ExplicitQueryStringParams.Count > 0)
                url += "?" + string.Join("&", ExplicitQueryStringParams.Select(x => $"{x.Key}={System.Web.HttpUtility.UrlEncode(x.Value)}"));
            // All the other parameters are serialized into the request body
            var msg = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(Params)
            };
            return msg;
        }

        /// <summary>
        /// Converts to HTTP GET request.
        /// </summary>
        /// <returns></returns>
        [Obsolete("ToGetRequest is deprecated, please use ToPostRequest instead.")]
        public HttpRequestMessage ToGetRequest()
        {
            // Merge all parameters into one collection
            var args = new List<KeyValuePair<string, string>>();
            foreach (var x in ExplicitQueryStringParams)
                args.Add(x);
            foreach (var x in Params)
                args.Add(x);
            var url = GetBaseUrl();
            if (args.Count > 0)
                url += "?" + string.Join("&", args.Select(x => string.IsNullOrEmpty(x.Value) ? x.Key : $"{x.Key}={System.Web.HttpUtility.UrlEncode(x.Value)}"));
            var msg = new HttpRequestMessage(HttpMethod.Get, url);
            return msg;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{GetBaseUrl()} ({ExplicitQueryStringParams.Count + Params.Count} parameters)";
        #endregion

        #region Private Methods
        private string GetBaseUrl() => Endpoint;
        #endregion
    }
}