# 🛠 Stage 1: Build the .NET 9 Application
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build-env
WORKDIR /app

# Copy the solution file and project files first to leverage Docker cache
COPY *.csproj . ./
RUN dotnet restore

# Copy the full source code
COPY . ./
RUN dotnet publish -c Release -o out

# 🏃 Stage 2: Runtime Image
FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5001

ENTRYPOINT ["dotnet", "Oficina.Hosts.Http.dll"]
