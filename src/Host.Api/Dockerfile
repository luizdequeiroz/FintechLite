FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY global.json ./
COPY src/Host.Api/Host.Api.csproj src/Host.Api/
RUN dotnet restore src/Host.Api/Host.Api.csproj

COPY . .
RUN dotnet publish src/Host.Api/Host.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Host.Api.dll"]
