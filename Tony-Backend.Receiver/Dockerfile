#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tony-Backend.Receiver/Tony-Backend.Receiver.csproj", "Tony-Backend.Receiver/"]
RUN dotnet restore "./Tony-Backend.Receiver/./Tony-Backend.Receiver.csproj"
COPY . .
WORKDIR "/src/Tony-Backend.Receiver"
RUN dotnet build "./Tony-Backend.Receiver.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tony-Backend.Receiver.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tony-Backend.Receiver.dll"]