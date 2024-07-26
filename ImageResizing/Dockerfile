# Use the official .NET image as the base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the csproj and restore any dependencies
COPY ImageResizing.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the application
COPY . ./
RUN dotnet publish ImageResizing.csproj -c Release -o out

# Use the official .NET runtime image as the base for the runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0

# Set the working directory
WORKDIR /app

# Copy the built application from the build image
COPY --from=build-env /app/out .

# Define the entry point for the application
ENTRYPOINT ["dotnet", "ImageResizing.dll"]