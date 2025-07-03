# 🛠 Stage 1: Build the .NET 9 Application
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build
WORKDIR /src

# Copy the solution file and project files first to leverage Docker cache
COPY CarePath.sln .
COPY Directory.Build.props ./
COPY Directory.Packages.props ./

COPY src/CarePath.Hosts.Http/*.csproj src/CarePath.Hosts.Http/
COPY src/CarePath.Domain/*.csproj src/CarePath.Domain/
COPY src/CarePath.Infrastructure/*.csproj src/CarePath.Infrastructure/
COPY src/CarePath.App.Api/*.csproj src/CarePath.App.Api/

# Restore dependencies
RUN dotnet restore

# Copy the full source code
COPY src ./src

# Build and publish
RUN dotnet publish src/CarePath.Hosts.Http/CarePath.Hosts.Http.csproj -c Release -o /app/out

# 🏃 Stage 2: Runtime Image
FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0 AS runtime
WORKDIR /app

# Add non-root user for security
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Copy published artifacts
COPY --from=build --chown=appuser:appuser /app/out .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5000
ENTRYPOINT ["dotnet", "CarePath.Hosts.Http.dll"]
