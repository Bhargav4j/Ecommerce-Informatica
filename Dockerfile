# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /src

# Copy solution file
COPY EcommerceInformatica.sln ./

# Copy project files for dependency restoration
COPY src/EcommerceInformatica.Domain/EcommerceInformatica.Domain.csproj ./src/EcommerceInformatica.Domain/
COPY src/EcommerceInformatica.Application/EcommerceInformatica.Application.csproj ./src/EcommerceInformatica.Application/
COPY src/EcommerceInformatica.Infrastructure/EcommerceInformatica.Infrastructure.csproj ./src/EcommerceInformatica.Infrastructure/
COPY src/EcommerceInformatica.Web/EcommerceInformatica.Web.csproj ./src/EcommerceInformatica.Web/

# Restore dependencies (cached layer)
RUN dotnet restore src/EcommerceInformatica.Web/EcommerceInformatica.Web.csproj

# Copy source code
COPY src/ ./src/

# Build application
RUN dotnet build src/EcommerceInformatica.Web/EcommerceInformatica.Web.csproj -c Release --no-restore

# Publish application
RUN dotnet publish src/EcommerceInformatica.Web/EcommerceInformatica.Web.csproj -c Release -o /app/publish --no-restore --no-build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Create non-root user for security
RUN groupadd -r appuser && useradd -r -g appuser appuser

# Copy published application from builder stage
COPY --from=builder /app/publish .

# Set ownership to non-root user
RUN chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Configure ASP.NET Core
ENV ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Expose application port
EXPOSE 8080

# Set entrypoint
ENTRYPOINT ["dotnet", "EcommerceInformatica.Web.dll"]