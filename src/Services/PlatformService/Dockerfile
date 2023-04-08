# Use the .NET SDK 7.0 image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory to /app
WORKDIR /app

# Copy the project file into the container
COPY PlatformService.csproj .

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code into the container
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use a smaller runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory to /app
WORKDIR /app

# Copy the build output from the build environment
COPY --from=build-env /app/out .

# Expose port 80 for the container
EXPOSE 80

# Set the entry point to the application
ENTRYPOINT ["dotnet", "PlatformService.dll"]
