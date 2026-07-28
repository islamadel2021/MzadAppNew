# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Expose the port that the application listens on
EXPOSE 80

# Copy the solution file and restore dependencies
COPY MzadApp.sln MzadApp.sln
COPY Backend/MzadService/MzadService.csproj Backend/MzadService/MzadService.csproj
COPY Backend/FilterService/FilterService.csproj Backend/FilterService/FilterService.csproj
COPY Backend/Contracts/Contracts.csproj Backend/Contracts/Contracts.csproj
COPY Backend/IdentityService/IdentityService.csproj Backend/IdentityService/IdentityService.csproj
COPY Backend/GatewayService/GatewayService.csproj Backend/GatewayService/GatewayService.csproj
COPY Backend/TenderService/TenderService.csproj Backend/TenderService/TenderService.csproj
COPY Backend/NotificationService/NotificationService.csproj Backend/NotificationService/NotificationService.csproj
COPY Tests/MzadService.IntegrationTests/MzadService.IntegrationTests.csproj Tests/MzadService.IntegrationTests/MzadService.IntegrationTests.csproj
COPY Tests/MzadService.UnitTests/MzadService.UnitTests.csproj Tests/MzadService.UnitTests/MzadService.UnitTests.csproj
RUN dotnet restore

# Copy the application folders
COPY Backend/IdentityService Backend/IdentityService
WORKDIR /app/Backend/IdentityService
RUN dotnet publish -c Release -o /app/Backend/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Copy the published output from the build image
WORKDIR /app
COPY --from=build /app/Backend/out .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "IdentityService.dll"]