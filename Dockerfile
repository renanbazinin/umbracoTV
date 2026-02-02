# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY umbraTV/umbraTV.csproj umbraTV/
RUN dotnet restore umbraTV/umbraTV.csproj

# Copy everything and build
COPY umbraTV/ umbraTV/
WORKDIR /src/umbraTV
RUN dotnet build umbraTV.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish umbraTV.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install SQLite (needed for runtime)
RUN apt-get update && apt-get install -y sqlite3 libsqlite3-dev && rm -rf /var/lib/apt/lists/*

# Copy published app
COPY --from=publish /app/publish .

# Copy Umbraco data directory with SQLite database
COPY umbraTV/umbraco/Data ./umbraco/Data

# Create necessary directories
RUN mkdir -p /app/umbraco/Data/TEMP/ExamineIndexes && \
    mkdir -p /app/umbraco/Data/TEMP/FileUploads && \
    mkdir -p /app/umbraco/Logs && \
    mkdir -p /app/wwwroot/media

# Cloud Run uses PORT environment variable
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "umbraTV.dll"]
