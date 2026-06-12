FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ServerDashboardApi/ServerDashboardApi.csproj", "ServerDashboardApi/"]
RUN dotnet restore "ServerDashboardApi/ServerDashboardApi.csproj"
COPY . .
WORKDIR "/src/ServerDashboardApi"
RUN dotnet publish "ServerDashboardApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ServerDashboardApi.dll"]