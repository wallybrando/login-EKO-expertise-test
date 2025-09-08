FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# ENV ASPNETCORE_HTTP_PORTS=8080
# ENV ASPNETCORE_HTTPS_PORTS=8081
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LoginEKO.FileProcessingService.Api/LoginEKO.FileProcessingService.Api.csproj", "LoginEKO.FileProcessingService.Api/"]
RUN dotnet restore "LoginEKO.FileProcessingService.Api/LoginEKO.FileProcessingService.Api.csproj"
COPY ./src/ ./
WORKDIR LoginEKO.FileProcessingService.Api
#WORKDIR "/src/LoginEKO.FileProcessingService.Api"
RUN dotnet build "LoginEKO.FileProcessingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LoginEKO.FileProcessingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish -p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoginEKO.FileProcessingService.Api.dll"]