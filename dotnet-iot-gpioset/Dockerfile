#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["dotnet-iot-gpioset.csproj", "."]
RUN dotnet restore "./dotnet-iot-gpioset.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnet-iot-gpioset.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnet-iot-gpioset.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnet-iot-gpioset.dll"]