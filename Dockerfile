FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .

COPY MatchPet.Api/*.csproj MatchPet.Api/
COPY MatchPet.Features/*.csproj MatchPet.Features/
COPY MatchPet.Infrastructure/*.csproj MatchPet.Infrastructure/
COPY MatchPet.Shared/*.csproj MatchPet.Shared/

RUN dotnet restore

COPY . .

RUN dotnet publish MatchPet.Api -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MatchPet.Api.dll"]