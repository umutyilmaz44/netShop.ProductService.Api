# BASE Stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# BUILD Stage
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
# Copy csproj and restore as distinct layers
# CORE.APPLICATION csproj file
COPY core/application/*.csproj core/application/
# CORE.DOMAIN csproj file
COPY core/domain/*.csproj core/domain/
# CORE.DOMAIN csproj file
COPY infrastructure/persistence/*.csproj infrastructure/persistence/
# CORE.DOMAIN csproj file
COPY webApi/webApi/*.csproj webApi/webApi/
# Copy everything else, except specified into dockerignore
RUN dotnet restore /app/webApi/webApi/*.csproj
COPY . .
RUN dotnet publish -c Release /app/webApi/webApi/*.csproj -o /app/dist

# FINAL Stage
FROM base AS final
WORKDIR /app

LABEL maintainer="Umit YILMAZ <umutyilmaz44@gmail.com>"

ENV ASPNETCORE_ENVIRONMENT=Development
ENV UseHttps=NO
ENV ASPNETCORE_HTTPS_PORT=5011
ENV DbSettings__Host=127.0.0.1
ENV DbSettings__Port=5432
ENV DbSettings__Username=postgres
ENV DbSettings__Password=password
ENV DbSettings__Database=NetShopDb
ENV DbSettings__DatabaseType=InMemory

ENV SsoSettings__Authority=https://localhost:5001
ENV SsoSettings__ValidIssuer=https://localhost:5001
ENV SsoSettings__ValidAudience=ProductService
ENV SsoSettings__IssuerSigningKey=qwertyuiopasdfghjklzxcvbnm123456

COPY --from=build /app/dist .
ENTRYPOINT ["dotnet", "netShop.ProductService.WebApi.dll"]