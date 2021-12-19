# SynologyDotNet.Core
**Base library** to develop .NET clients for Synology DSM.

* Target: **.NET Standard 2.0**, so it works with a wide range of .NET versions.
* This is a small framework to consume **Synology DSM's Web API**
* Supports **HTTP** and **HTTPS**
* SSL certificate validation can be disabled (only for SynoClient, does not affect the AppDomain)

## NuGet packages


### [SynologyDotNet.Core](https://www.nuget.org/packages/SynologyDotNet.Core/)
This is the base library, the core. All the specific clients are built on top of this.
```
Install-Package SynologyDotNet.Core
```

### [SynologyDotNet.AudioStation](https://www.nuget.org/packages/SynologyDotNet.AudioStation/)
```
Install-Package SynologyDotNet.AudioStation
```

More coming soon as I make progress... But you also have the option to write your own! :)


## Usage examples

### Query supported APIs from the Synology NAS
This C# snippet connects to a Synology NAS and lists all supported APIs.  
```
var client = new SynoClient("http://MySynolgyNAS:5000/");
var apis = await client.QueryApiInfos();
foreach (var api in apis)
    Debug.WriteLine(api.ToString());
```

Output:
```
Name=SYNO.API.Auth, MaxVersion=7, Path=entry.cgi, RequestFormat=
Name=SYNO.API.Auth.Key, MaxVersion=7, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.Auth.Key.Code, MaxVersion=7, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.Auth.RedirectURI, MaxVersion=1, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.Auth.Type, MaxVersion=1, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.Auth.UIConfig, MaxVersion=1, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.Encryption, MaxVersion=1, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.Info, MaxVersion=1, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.API.OTP, MaxVersion=1, Path=otp.cgi, RequestFormat=
Name=SYNO.AudioPlayer, MaxVersion=2, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.AudioPlayer.Stream, MaxVersion=2, Path=entry.cgi, RequestFormat=JSON
Name=SYNO.AudioStation.Album, MaxVersion=3, Path=AudioStation/album.cgi, RequestFormat=
Name=SYNO.AudioStation.Artist, MaxVersion=4, Path=AudioStation/artist.cgi, RequestFormat=
Name=SYNO.AudioStation.Browse.Playlist, MaxVersion=1, Path=entry.cgi, RequestFormat=JSON
...
```

### Basic example with one client

In order to consume data, you may also add other NuGet packages like **SynologyDotNet.AudioStation**.
This example shows how to configure the connection and login with username and password.  

```
// Create an AudioStationClient
var audioStation = new AudioStationClient();

// Create the SynoClient which communicates with the server, this can be re-used across all Station Clients.
var client = new SynoClient(new Uri("https://MySynolgyNAS:5001/"), audioStation);

// Login
await client.LoginAsync("username", "password");

// Get 100 artists from the music library.
var response = await audioStation.ListArtistsAsync(100, 0);
foreach(var artist in response.Data.Artists)
    Console.WriteLine(artist.Name);
```

### Login by re-using previous session

SynoClient supports re-using sessions. This is possible if you didn't call the LogoutAsync 
function and you saved the session returned from LoginAsync function like this:

```
if(synoSession is null)
    synoSession = await _synoClient.LoginAsync("username", "password");
else
    await _synoClient.LoginWithPreviousSessionAsync(synoSession);
```

The LoginWithPreviousSessionAsync function will perform a test by default to validate the session,
but this can be disabled optionally if you want to save one request.  
```
    await _synoClient.LoginWithPreviousSessionAsync(synoSession, false);
```
If you make a request anyway after login to fetch your data, this is the recommended approach.


## How to begin development?

### SynoClient
The idea is that **one SynoClient serves multiple connectors**, so only one instance of HttpClient 
is utilized under the hood asynchronously. Also a huge benefit that you have to 
authenticate once and all the connectors will use the same user session once the SynoClient 
is logged in.  

SynoClient does the followings:
- User login with username and password
- Automatically gets the Cookie and SynoToken from the NAS and uses it in HTTP requests
- By default, HTTP POST requests used only, and this is the recommended method for security reasons
- Re-usable user session
- API specification handling
- Base data model with generic types

### API
First, query all supported APIs to see what the NAS can do. 
Then create a list of API names you need, for example in the case of my AudioStationClient:  
- SYNO.AudioStation.Info
- SYNO.AudioStation.Album
- SYNO.AudioStation.Composer
- SYNO.AudioStation.Genre
- SYNO.AudioStation.Artist
- SYNO.AudioStation.Folder
- SYNO.AudioStation.Song
-  .. and so on

### New connector derives from StationConnectorBase
1. Create a new .NET project in Visual Studio
2. Add [SynologyDotNet.Core](https://www.nuget.org/packages/SynologyDotNet.Core/)
NuGet package to the project
3. Create a new class named *MyConnector* derived from *StationConnectorBase*
4. Implement abstract members

#### Implement string[] GetImplementedApiNames()

This method must return the list of API names your connector will use like this:  
```
protected override string[] GetImplementedApiNames() => new string[]
{
    "SYNO.AudioStation.Info",
    "SYNO.AudioStation.Album",
    "SYNO.AudioStation.Composer",
    "SYNO.AudioStation.Genre",
    "SYNO.AudioStation.Artist",
    "SYNO.AudioStation.Folder",
    "SYNO.AudioStation.Song",
    "SYNO.AudioStation.Cover",
    "SYNO.AudioStation.Stream",
    "SYNO.AudioStation.Search",
    "SYNO.AudioStation.Lyrics",
    "SYNO.AudioStation.Playlist"
};
```

### Do something, utilize the framework
I'll stick to my *AudioStationClient* example here to **fetch a list of artists**:  
```
public async Task<ApiListRessponse<ArtistList>> ListArtistsAsync(int limit, int offset)
{
    return await Client.QueryListAsync<ApiListRessponse<ArtistList>>(
        "SYNO.AudioStation.Artist",  // API name
        "list",                      // API method (controller)
        limit,                       // Pagination: page size
        offset                       // Pagination: page offset
    );
}
```
Quite short, isn't it? The followings are happening here:  
- The framework knows what and where to call by the API name.
- The HTTP request is constructed under the hood
- The response JSON is parsed to a an ArtistList
- Pagination supported by default if you're using `ApiListRessponse<T>`

Here is the **data model** returned by `ListArtistsAsync`: 
```
public class ArtistList : ListResponseBase
{
    [JsonProperty("artists")]
    public Artist[] Artists { get; set; }
}

public class Artist
{
    [JsonProperty("name")]
    public string Name { get; set; }

    public override string ToString() => Name ?? base.ToString();
}
```

### API parameters, data model?

How do I know the parameters and the data model? Well... I spent some time with **Fiddler** to 
**reverse engineer** the original AudioStation webapplication **API calls** :)  


## Query methods in SynoClient

### RequestBuilder

When you call the Synology API, the RequestBuilder is used to build the actual HTTP request from various parameters.  
SynoClient provides lower level methods which accept a RequestBuilder, so you have the option to construct your query manually.  
On the other hand, you have "convenience" methods with a simplified argument list for basic stuff.  

### Simple methods

`Task<T> QueryListAsync<T>(string apiName, string method, int limit, int offset, params (string, object)[] parameters)`  

`Task<T> QueryObjectAsync<T>(string apiName, string method, params (string, object)[] parameters)`  

`Task<ByteArrayData> QueryByteArrayAsync(string apiName, string method, params (string, object)[] parameters)`  

### Lower level methods with RequestBuilder

`Task<string> QueryStringAsync(RequestBuilder req)`  

`Task<T> QueryObjectAsync<T>(RequestBuilder req)`  

`Task<ByteArrayData> QueryByteArrayAsync(RequestBuilder req)`  

`Task QueryStreamAsync(RequestBuilder req, Action<StreamResult> readStreamAction)`  

## Notes

This is one of my home projects. My goal is to develop my own library and build applications on top of them. 
It can be useful for others too, so I decided to go open-source and publish my packages.

**Always make backups. I am not responsible for any data loss.**
