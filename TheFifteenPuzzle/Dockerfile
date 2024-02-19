FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TheFifteenPuzzle/TheFifteenPuzzle.csproj", "TheFifteenPuzzle/"]
RUN dotnet restore "TheFifteenPuzzle/TheFifteenPuzzle.csproj"
COPY . .
WORKDIR "/src/TheFifteenPuzzle"
RUN dotnet build "TheFifteenPuzzle.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TheFifteenPuzzle.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheFifteenPuzzle.dll"]
