FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CouchDb/CouchDb.csproj", "CouchDb/"]
RUN dotnet restore "CouchDb/CouchDb.csproj"
COPY . .
WORKDIR "/src/CouchDb"
RUN dotnet build "CouchDb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CouchDb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CouchDb.dll"]
