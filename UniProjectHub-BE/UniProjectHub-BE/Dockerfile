# Use the ASP.NET Core runtime image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# Expose HTTP ports
EXPOSE 8080
EXPOSE 8081

# Set environment variable to use HTTP
ENV ASPNETCORE_URLS=http://+:8080

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["UniProjectHub-BE/UniProjectHub-BE.csproj", "UniProjectHub-BE/"]
COPY ["Infracstructures/Infracstructures.csproj", "Infracstructures/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "./UniProjectHub-BE/UniProjectHub-BE.csproj"

# Copy the entire solution and build the application
COPY . .
WORKDIR "/src/UniProjectHub-BE"
RUN dotnet build "./UniProjectHub-BE.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UniProjectHub-BE.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniProjectHub-BE.dll"]
