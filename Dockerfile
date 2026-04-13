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
COPY ["src/Services/Ecosystem/CRM/ZAP.Ecosystem.API.CRM/ZAP.Ecosystem.API.CRM.csproj", "src/Services/Ecosystem/CRM/ZAP.Ecosystem.API.CRM/"]
COPY ["src/Services/Ecosystem/CRM/ZAP.Ecosystem.Application.CRM/ZAP.Ecosystem.Application.CRM.csproj", "src/Services/Ecosystem/CRM/ZAP.Ecosystem.Application.CRM/"]
COPY ["src/Services/Ecosystem/ZAP.Ecosystem.Infrastructure/ZAP.Ecosystem.Infrastructure.csproj", "src/Services/Ecosystem/ZAP.Ecosystem.Infrastructure/"]
COPY ["src/Services/Ecosystem/ZAP.Ecosystem.Domain/ZAP.Ecosystem.Domain.csproj", "src/Services/Ecosystem/ZAP.Ecosystem.Domain/"]
COPY ["src/Services/Ecosystem/ZAP.Ecosystem.Shared/ZAP.Ecosystem.Shared.csproj", "src/Services/Ecosystem/ZAP.Ecosystem.Shared/"]

# Restore dependencies
RUN dotnet restore "src/Services/Ecosystem/CRM/ZAP.Ecosystem.API.CRM/ZAP.Ecosystem.API.CRM.csproj"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/src/Services/Ecosystem/CRM/ZAP.Ecosystem.API.CRM"

# Build the project
RUN dotnet build "ZAP.Ecosystem.API.CRM.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ZAP.Ecosystem.API.CRM.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZAP.Ecosystem.API.CRM.dll"]
