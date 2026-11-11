# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HonyStore.API/HonyStore.API.csproj", "src/HonyStore.API/"]
COPY ["src/HonyStore.Application/HonyStore.Application.csproj", "src/HonyStore.Application/"]
COPY ["src/HonyStore.Domain/HonyStore.Domain.csproj", "src/HonyStore.Domain/"]
COPY ["src/HonyStore.Infrastructure/HonyStore.Infrastructure.csproj", "src/HonyStore.Infrastructure/"]
RUN dotnet restore "./src/HonyStore.API/HonyStore.API.csproj"
COPY . .
WORKDIR "/src/src/HonyStore.API"
RUN dotnet build "./HonyStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HonyStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HonyStore.API.dll"]