FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["VeterinariaGenesis.Api/VeterinariaGenesis.Api.csproj", "VeterinariaGenesis.Api/"]
COPY ["VeterinariaGenesis.Application/VeterinariaGenesis.Application.csproj", "VeterinariaGenesis.Application/"]
COPY ["VeterinariaGenesis.Domain/VeterinariaGenesis.Domain.csproj", "VeterinariaGenesis.Domain/"]
COPY ["VeterinariaGenesis.Infrastructure/VeterinariaGenesis.Infrastructure.csproj", "VeterinariaGenesis.Infrastructure/"]

RUN dotnet restore "VeterinariaGenesis.Api/VeterinariaGenesis.Api.csproj"

COPY . .
WORKDIR "/src/VeterinariaGenesis.Api"
RUN dotnet build "VeterinariaGenesis.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinariaGenesis.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinariaGenesis.Api.dll"]
