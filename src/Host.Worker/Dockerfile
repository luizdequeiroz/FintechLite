FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY global.json ./
COPY src/Host.Worker/Host.Worker.csproj src/Host.Worker/
RUN dotnet restore src/Host.Worker/Host.Worker.csproj

COPY . .
RUN dotnet publish src/Host.Worker/Host.Worker.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Host.Worker.dll"]
