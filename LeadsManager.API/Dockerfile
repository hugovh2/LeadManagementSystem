FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LeadsManager.API/LeadsManager.API.csproj", "LeadsManager.API/"]
COPY ["LeadsManager.Application/LeadsManager.Application.csproj", "LeadsManager.Application/"]
COPY ["LeadsManager.Domain/LeadsManager.Domain.csproj", "LeadsManager.Domain/"]
COPY ["LeadsManager.Infrastructure/LeadsManager.Infrastructure.csproj", "LeadsManager.Infrastructure/"]
RUN dotnet restore "LeadsManager.API/LeadsManager.API.csproj"
COPY . .
WORKDIR "/src/LeadsManager.API"
RUN dotnet build "LeadsManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeadsManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeadsManager.API.dll"]
