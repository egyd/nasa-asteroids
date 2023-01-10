The basic API endpoint that provides 3 most dangerous asteroids data based on NASA API information.

How to run:
- install dotnet v6
- clone the repo
- cd %repository%
- dotnet run --project src/Asteroids.Api/Asteroids.Api.csproj
- curl -k "https://localhost:7217/asteroids?days=3"