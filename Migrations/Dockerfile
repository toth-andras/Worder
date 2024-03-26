FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

ENV DB_CONNECTION_STRING_DEBUG="Username=postgres;Password=postgres;Server=localhost;Database=postgres;Port=5432"
ENV DB_CONNECTION_STRING_RELEASE=""
ENV RELEASE_MODE=false
ENV MIGRATE_UP=true
ENV MIGRATE_DOWN_TO=0

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Migrations/Migrations.csproj", "Migrations/"]
RUN dotnet restore "Migrations/Migrations.csproj"
COPY . .
WORKDIR "/src/Migrations"
RUN dotnet build "Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrations.dll"]
