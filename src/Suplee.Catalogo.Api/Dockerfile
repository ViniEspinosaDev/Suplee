FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Suplee.Catalogo.Api.csproj", "Suplee.Catalogo.Api/"]
RUN dotnet restore "Suplee.Catalogo.Api/Suplee.Catalogo.Api.csproj"
COPY . .
#WORKDIR "/src/Suplee.Catalogo.Api"
RUN dotnet build "Suplee.Catalogo.Api/Suplee.Catalogo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Suplee.Catalogo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Suplee.Catalogo.Api.dll"]
