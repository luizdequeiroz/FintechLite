FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY global.json ./
COPY src/Workers/Workers.csproj src/Workers/
RUN dotnet restore src/Workers/Workers.csproj

COPY . .
RUN dotnet publish src/Workers/Workers.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Workers.dll"]
