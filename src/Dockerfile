FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Suplee.Api/Suplee.Api.csproj"
COPY . .
WORKDIR "/src/Suplee.Api"
RUN dotnet build "Suplee.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Suplee.Api"
RUN dotnet publish "Suplee.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Suplee.Api.dll
