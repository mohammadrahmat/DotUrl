# DotUrl

DotUrl is a converter service for converting trendyol weburls into deeplinks and the reverse operation, designed using .net core.

## Installation

Prerequisites:
- Docker
- Dotnet Core Sdk 3.1
- Necessary nuget configs if you are connected behind a proxy

Use docker composer to build and install elastic search and kibana 

```powershell
docker-compose build
docker-compose up
```

After elastic and kibana containers are up and running, run the app:

```powershell
dotnet run --project path-to-project/DotUrl.csproj
```

## Usage
The app has swagger, to trigger endpoints via swagger just need to access it via browser:

```
http://localhost:5001/swagger
```

both end points need a string input, returns a string response and are POST services.

###### Url-To-Deeplink Converter Endpoint
```
http://localhost:5001/UrlService/convert
```

###### Deelink-To-Url Converter Endpoint
```
http://localhost:5001/DeeplinkService/convert
```

## TODOs:
Things to do in the future if had the chance and time:

- Write a batch file to wait for elasticsearch status when automizing app builds
- Fix the docker-compose network issue when trying to run the app using multi stage build
- Set environment variables, after the above 2 were done
- Maybe store data such as product name, category, etc and use it to make better urls
- Uhhh, didn't realize i did not group test projects under a folder, should do that!
