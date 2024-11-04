FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentaCar.Vehicles/RentaCar.Vehicles.WebAPI/RentaCar.Vehicles.WebAPI.csproj", "RentaCar.Vehicles/RentaCar.Vehicles.WebAPI/"]
COPY ["RentaCar.Vehicles/RentaCar.Vehicles.Application/RentaCar.Vehicles.Application.csproj", "RentaCar.Vehicles/RentaCar.Vehicles.Application/"]
COPY ["RentaCar.Vehicles/RentaCar.Vehicles.Domain/RentaCar.Vehicles.Domain.csproj", "RentaCar.Vehicles/RentaCar.Vehicles.Domain/"]
COPY ["RentaCar.Vehicles/RentaCar.Vehicles.Infrastructure/RentaCar.Vehicles.Infrastructure.csproj", "RentaCar.Vehicles/RentaCar.Vehicles.Infrastructure/"]
RUN dotnet restore "./RentaCar.Vehicles/RentaCar.Vehicles.WebAPI/RentaCar.Vehicles.WebAPI.csproj"
COPY . .
WORKDIR "/src/RentaCar.Vehicles/RentaCar.Vehicles.WebAPI"
RUN dotnet build "./RentaCar.Vehicles.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentaCar.Vehicles.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "RentaCar.Vehicles.WebAPI.dll"]