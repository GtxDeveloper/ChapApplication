FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AudioServer/AudioServer.csproj", "AudioServer/"]
RUN dotnet restore "AudioServer/AudioServer.csproj"
COPY . .
WORKDIR "/src/AudioServer"
RUN dotnet build "AudioServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AudioServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudioServer.dll"]
