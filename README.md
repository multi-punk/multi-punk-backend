# How to start?
You should to build it via this command:
just build:
```cli
dotnet run --project Api
```
for docker:
```cli
docker build {name-for-image} .
```
After that create docker-compose file, for example:
```yml
version: "3.9"

services: 
  site:
    image: {image-name}
    volumes:
        -{path-on-system-to-mount}:/App/Settings
```
For normal work this site need to give psql data to docker environ, or appsettings.json, if you download this project from github manually
Example data for appsettings.json:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "connectionOnServer": "Host=localhost;Username={NameOfDb};Password={Password};Database=postgres;Port=5432",
  "AllowedHosts": "*"
}
```