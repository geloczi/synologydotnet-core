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


## Usage examples

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

## Notes

This is one of my home projects. My goal is to develop my own library and build applications on top of them. 
It can be useful for others too, so I decided to go open-source and publish my packages.

**Always make backups. I am not responsible for any data loss.**
