FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["VeterinariaGenesis.Api/VeterinariaGenesis.Api.csproj", "VeterinariaGenesis.Api/"]
COPY ["VeterinariaGenesis.Application/VeterinariaGenesis.Application.csproj", "VeterinariaGenesis.Application/"]
COPY ["VeterinariaGenesis.Domain/VeterinariaGenesis.Domain.csproj", "VeterinariaGenesis.Domain/"]
COPY ["VeterinariaGenesis.Infrastructure/VeterinariaGenesis.Infrastructure.csproj", "VeterinariaGenesis.Infrastructure/"]
COPY ["VeterinariaGenesis.Client/VeterinariaGenesis.Client.csproj", "VeterinariaGenesis.Client/"]

RUN dotnet restore "VeterinariaGenesis.Api/VeterinariaGenesis.Api.csproj"
RUN dotnet restore "VeterinariaGenesis.Client/VeterinariaGenesis.Client.csproj"

COPY . .

# Publicar Cliente Blazor WASM
FROM build AS publish_client
WORKDIR "/src/VeterinariaGenesis.Client"
RUN dotnet publish "VeterinariaGenesis.Client.csproj" -c Release -o /app/client_publish

# Publicar API
FROM build AS publish_api
WORKDIR "/src/VeterinariaGenesis.Api"
RUN dotnet publish "VeterinariaGenesis.Api.csproj" -c Release -o /app/api_publish /p:UseAppHost=false

# Imagen final: API sirve también el Cliente como archivos estáticos
FROM base AS final
WORKDIR /app
COPY --from=publish_api /app/api_publish .
COPY --from=publish_client /app/client_publish/wwwroot ./wwwroot
ENTRYPOINT ["dotnet", "VeterinariaGenesis.Api.dll"]
