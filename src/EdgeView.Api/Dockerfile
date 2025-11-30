# -----------------------------
# 1. Build stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution
COPY EdgeViewBackEnd.sln .

# Copy csproj files
COPY src/EdgeView.Api/EdgeView.Api.csproj src/EdgeView.Api/
COPY src/EdgeView.Application/EdgeView.Application.csproj src/EdgeView.Application/
COPY src/EdgeView.Domain/EdgeView.Domain.csproj src/EdgeView.Domain/
COPY src/EdgeView.Infrastructure/EdgeView.Infrastructure.csproj src/EdgeView.Infrastructure/

# Restore dependencies
RUN dotnet restore src/EdgeView.Api/EdgeView.Api.csproj

# Sorting Playwright for docker
RUN dotnet tool install --global Microsoft.Playwright.CLI

# Ensure the dotnet global tools path is available
ENV PATH="$PATH:/root/.dotnet/tools"

# Install Playwright browsers (with dependencies)
RUN playwright install --with-deps


# Copy everything
COPY src/ ./src/

# Build & publish
RUN dotnet publish src/EdgeView.Api/EdgeView.Api.csproj -c Release -o /app/publish

# -----------------------------
# 2. Runtime stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "EdgeView.Api.dll"]
