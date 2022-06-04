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

 ## To build docker image:
 * cd projectPath/
 * docker image build -t product_service_api .

## Create Developer Certification to run image with Https:
 For MacOS
 * dotnet dev-certs https -ep ${HOME}/.aspnet/https/netShop.ProductService.WebApi.pfx -p netProduct123.
 * dotnet dev-certs https --trust

## To run local docker image with HTTPS:
 * docker container run --rm -p 5010:80 -p 5011:443 --name c_product_service_api \
-e DbSettings__Host=192.168.0.10 -e SsoSettings__Authority=https:/192.168.0.20:5000 -e UseHttps=yes \
-e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5011 -e ASPNETCORE_Kestrel__Certificates__Default__Password="netProduct123." \
-e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/netShop.ProductService.WebApi.pfx -v ${HOME}/.aspnet/https:/https/ \
product_service_api

## To run docker hub image with HTTPS:
 * docker container run --rm -p 5010:80 -p 5011:443 --name c_product_service_api \
-e DbSettings__Host=192.168.0.10 -e SsoSettings__Authority=https://192.168.0.20:5000 -e UseHttps=yes \
-e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5011 -e ASPNETCORE_Kestrel__Certificates__Default__Password="netProduct123." \
-e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/netShop.ProductService.WebApi.pfx -v ${HOME}/.aspnet/https:/https/ \
uyilmaz/product_service_api

## To run local docker image with ONLY HTTP:
* docker container run --rm -p 5010:80 --name c_product_service_api \
-e DbSettings__Host=192.168.0.10 -e SsoSettings__Authority=https://192.168.0.20:5000 -e UseHttps=no \
-e ASPNETCORE_URLS="http://+" product_service_api

## To run docker hub image with ONLY HTTP:
* docker container run --rm -p 5010:80 --name c_product_service_api \
-e DbSettings__Host=192.168.0.10 -e SsoSettings__Authority=https://192.168.0.20:5000 -e UseHttps=no \
-e ASPNETCORE_URLS="http://+" \
uyilmaz/product_service_api

## To run docker app with local docker-compose:
* docker-compose up

## NOTICE :
* You can change certifitaion passsword {netProduct123.} and 5010, 5011 ports what you want.

* ALL PARAMETERS OF THE APPLICATION HAS DESCRIBED ON MY DOCKER HUB IMAGE. FOR DETAIL, YOU CAN VISIT [uyilmaz/product_service_api](https://hub.docker.com/r/uyilmaz/product_service_api).
 
