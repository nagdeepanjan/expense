# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies (for better layer caching)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./

# Build and publish the app in release mode
RUN dotnet publish -c Release -o /out --no-restore

# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create a non-root user for security
RUN adduser --disabled-password --gecos '' --shell /bin/bash --home /home/appuser appuser

# Copy the published app from the build stage
COPY --from=build /out ./

# Change ownership of the app directory to the appuser
RUN chown -R appuser:appuser /app

# Switch to the non-root user
USER appuser

# Expose the port the app runs on
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=30s --retries=3 \
    CMD curl -f http://localhost:8080/Expense || exit 1

# Set the entry point
ENTRYPOINT ["dotnet", "Expense.dll"]
