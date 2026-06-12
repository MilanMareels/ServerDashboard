FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ServerDashboardClient/ServerDashboardClient.csproj", "ServerDashboardClient/"]
COPY ["ServerDashboardApi/ServerDashboardApi.csproj", "ServerDashboardApi/"]
RUN dotnet restore "ServerDashboardClient/ServerDashboardClient.csproj"
COPY . .
WORKDIR "/src/ServerDashboardClient"
RUN dotnet publish "ServerDashboardClient.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ServerDashboardClient.dll"]