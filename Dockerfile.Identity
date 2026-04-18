# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["ZAP.Ecosystem.slnx", "."]
COPY ["src/Services/Identity/ZAP.Identity.API/ZAP.Identity.API.csproj", "src/Services/Identity/ZAP.Identity.API/"]
COPY ["src/Services/Identity/ZAP.Identity.Application/ZAP.Identity.Application.csproj", "src/Services/Identity/ZAP.Identity.Application/"]
COPY ["src/Services/Identity/ZAP.Identity.Infrastructure/ZAP.Identity.Infrastructure.csproj", "src/Services/Identity/ZAP.Identity.Infrastructure/"]
COPY ["src/Services/Identity/ZAP.Identity.Domain/ZAP.Identity.Domain.csproj", "src/Services/Identity/ZAP.Identity.Domain/"]
COPY ["src/Services/Ecosystem/ZAP.Ecosystem.Shared/ZAP.Ecosystem.Shared.csproj", "src/Services/Ecosystem/ZAP.Ecosystem.Shared/"]

# Restore dependencies
RUN dotnet restore "src/Services/Identity/ZAP.Identity.API/ZAP.Identity.API.csproj"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/src/Services/Identity/ZAP.Identity.API"

# Build the project
RUN dotnet build "ZAP.Identity.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ZAP.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZAP.Identity.API.dll"]
