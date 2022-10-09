FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

## Environment Variables
ENV ASPNETCORE_ENVIRONMENT Production

# SQL 
ENV SQL_SERVER suplee.database
ENV SQL_NAME_DB supleedatabase
ENV SQL_USER sa
ENV SQL_PASSWORD ea!@#12345
ENV SQL_APP_NAME Suplee

# IMGBB
ENV IMGBB_URL https://api.imgbb.com
ENV IMGBB_EXPIRATION 600
ENV IMGBB_KEY 5f6e7b6c08b780e10bdbbd3ed9ae7d6b

# MAIL SERVICE
ENV MAIL_SMTP smtp.gmail.com
ENV MAIL_PORT 587
ENV MAIL_ADDRESS suplee.app@gmail.com
ENV MAIL_PASSWORD gxfnpqxpcdhaiedm
ENV MAIL_USESSL true

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
