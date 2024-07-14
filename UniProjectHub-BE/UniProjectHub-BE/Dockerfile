# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore all projects first to leverage Docker cache
COPY UniProjectHub-BE/UniProjectHub-BE.csproj ./UniProjectHub-BE/
COPY Domain/Domain.csproj ./Domain/
COPY Application/Application.csproj ./Application/
COPY Infracstructures/Infracstructures.csproj ./Infracstructures/

RUN dotnet restore ./UniProjectHub-BE/UniProjectHub-BE.csproj

# Copy full solution and build
COPY . .
WORKDIR "/src/UniProjectHub-BE"
RUN dotnet build "./UniProjectHub-BE.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "./UniProjectHub-BE.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "UniProjectHub-BE.dll"]
