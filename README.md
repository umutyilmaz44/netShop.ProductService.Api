# netShop.ProductService
## Microservices Architecture (RestAPI) on .NET 5.0 with applying CQRS, Clean Architecture and Event-Driven Communication

 which includes; 
 * ASP.NET Core Web API application
 * REST API principles, CRUD operations
 * CQRS, MediatR and DDD pattern implementation
 * Global Exception Handling Mechanism implementation
 * Repository Pattern with UnitOfWork
 * Central Log Mechanism GrayLog with Serilog
 * Swagger Open API implementation
 * Authentication & Authorization with Jwt-SSO 
 * Containerized Application
 
 **NOTICE:** 
 * This project needs to a sso application (ex: identityserver4) for authentication and authorization. You must set **ApiResource="ProductService"**, **ApiScopes=["ProductService.Write","ProductService.Read"]** in the sso config
 * Moreover, 
   * ***```ssoAddress```*** word in the this document means that is a sso applicaiton url or service name in cloud network. (ex: htttp://192.168.0.10:5001 OR identityService in docker swarm, kubernetes, openshift, vs...)
   * ***```postgresqlAddress```*** word in the this document means that is a postgresql url or service name in cloud network. (ex: htttp://192.168.0.20 OR dbService in docker swarm, kubernetes, openshift, vs...)
   * ***```postgresqlDataPath```*** word in the this document means that is your persist postgresql data's path (ex: ${HOME}/netShop/productService/db/data)

 ## To Work With Postgresql
 * cd infrastructure/persistence/
 * ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add [migration name]
 * ASPNETCORE_ENVIRONMENT=Development dotnet ef database update
 * docker run -d --rm --network netshop-network -p 5432:5432 --name c_netshop_product_service_db \
-e POSTGRES_PASSWORD=netshop -e POSTGRES_USER=postgres -e POSTGRES_DB=NetShopProductDb \
-v ***```postgresqlDataPath```***:/var/lib/postgresql/data \
postgres

 ## To build docker image:
 * cd projectPath/
 * docker image build -t netshop_product_service_api .

## Create Developer Certification to run image with Https:
 For MacOS
 * dotnet dev-certs https -ep ${HOME}/.aspnet/https/netShop.ProductService.WebApi.pfx -p netProduct123.
 * dotnet dev-certs https --trust

## To run local docker image with HTTPS:
 * docker container run --rm -p 5010:80 -p 5011:443 --name c_netshop_product_service_api \
-e DbSettings__Host=***```postgresqlAddress```*** -e SsoSettings__Authority=***```ssoAddress```*** -e UseHttps=yes \
-e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5011 -e ASPNETCORE_Kestrel__Certificates__Default__Password="netProduct123." \
-e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/netShop.ProductService.WebApi.pfx -v ${HOME}/.aspnet/https:/https/ \
netshop_product_service_api

## To run docker hub image with HTTPS:
 * docker container run --rm -p 5010:80 -p 5011:443 --name c_netshop_product_service_api \
-e DbSettings__Host=***```postgresqlAddress```*** -e SsoSettings__Authority=***```ssoAddress```*** -e UseHttps=yes \
-e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5011 -e ASPNETCORE_Kestrel__Certificates__Default__Password="netProduct123." \
-e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/netShop.ProductService.WebApi.pfx -v ${HOME}/.aspnet/https:/https/ \
uyilmaz/netshop_product_service_api

## To run local docker image with ONLY HTTP:
* docker container run --rm -p 5010:80 --name c_netshop_product_service_api \
-e DbSettings__Host=***```postgresqlAddress```*** -e SsoSettings__Authority=***```ssoAddress```*** -e UseHttps=no \
-e ASPNETCORE_URLS="http://+" netshop_product_service_api

## To run docker hub image with ONLY HTTP:
* docker container run --rm -p 5010:80 --name c_netshop_product_service_api \
-e DbSettings__Host=***```postgresqlAddress```*** -e SsoSettings__Authority=***```ssoAddress```*** -e UseHttps=no \
-e ASPNETCORE_URLS="http://+" \
uyilmaz/netshop_product_service_api

## To run docker app with local docker-compose:
* docker-compose up
```yml
version: '3.7'

services:
  c_netshop_product_service_db:
    image: postgres
    restart: on-failure
    environment:
      - POSTGRES_PASSWORD=netshop
      - POSTGRES_USER=postgres 
      - POSTGRES_DB=NetShopProductDb
    ports:
      - "5432:5432"
    volumes:
      - postgresqlDataPath:/var/lib/postgresql/data
    networks:
      - netshop-network
      
  c_netshop_product_service_api:
    image: netshop_product_service_api
    depends_on:
      - "c_db_service"
    restart: on-failure
    environment:
      - DbSettings__DatabaseType=Postgresql
      - DbSettings__Host=c_netshop_product_service_db
      - DbSettings__Port=5432
      - DbSettings__Username=postgres
      - DbSettings__Password=netshop
      - DbSettings__Database=NetShopProductDb
      - SsoSettings__Authority= ssoAddress
      - UseHttps=yes
      - ASPNETCORE_URLS=https://+;http://+
      - ASPNETCORE_HTTPS_PORT=5011
      - ASPNETCORE_Kestrel__Certificates__Default__Password=netProduct123.
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/netShop.ProductService.WebApi.pfx
    ports:
      - "5010:80"
      - "5011:443"
    volumes:
      - ${HOME}/.aspnet/https:/https/
    networks:
      - netshop-network

networks:
  netshop-network: {}
```

## NOTICE :
* You can change certifitaion passsword {netProduct123.} and 5010, 5011 ports what you want.

* ALL PARAMETERS OF THE APPLICATION HAS DESCRIBED ON MY DOCKER HUB IMAGE. FOR DETAIL, YOU CAN VISIT [uyilmaz/netshop_product_service_api](https://hub.docker.com/r/uyilmaz/netshop_product_service_api).
 
