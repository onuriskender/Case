# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Blog.WebApi/Blog.WebApi.csproj", "Blog.WebApi/"]
RUN dotnet restore "Blog.WebApi/Blog.WebApi.csproj"
COPY . .
WORKDIR "/src/Blog.WebApi"
RUN dotnet build "Blog.WebApi.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Blog.WebApi.csproj" -c Release -o /app/publish

# Final stage / image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.WebApi.dll"]