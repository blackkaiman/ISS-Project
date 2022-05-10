# .NET Workshop - CRUD WebAPI project

## Create new WebAPI project

* ``` dotnet --info ```
* ``` dotnet new webapi -f net5.0 ```
* ``` dotnet new gitignore ```

## Building and running project

* ``` dotnet build ```
* ``` dotnet run ```
* ``` dotnet run watch ```
* ``` dotnet dev-certs https --trust ```

## WebAPI project overview

* .csproj file - framework and packages
* Program and Startup classes - entry into API

## EF code first and Db connection

* Create Model with properties
* Add EF Core packages
    * ``` Microsoft.EntityFrameworkCore.Sqlite ```
    * ``` Microsoft.EntityFrameworkCore.Design ```
* Add DB connection strings - appsettings.json
* Create DbContext class
* Add DbContext to startup
* Install dotnet-ef: ``` dotnet tool install --global dotnet-ef --version 5.0.0 ```
* Add Migration: ``` dotnet ef migrations add InitialCreate ```
* Update Database: ``` dotnet ef database update ```
* (Explore DB and add example entries in Db)

## Repository (service)

* Create service interface with GET method signatures
* Implement service interface with GET methods logic
* Add scoped service to startup

## Controller (endpoint)

* Create controller and add GET endpoints
* Inject and use service to read from DB

## DTOs

* Add AutoMapper packages
    * ``` AutoMapper ```
    * ``` AutoMapper.Extensions.Microsoft.DependencyInjection ```
* Create GET DTO without ID property
* Add Mapping profile from model to DTO
* Add AutoMapper to startup
* Map service result to DTO

## POST endpoint

* Add POST method to interface and implement method in service
* Check for existing entity before creating
* Add POST method to Controller
* Add [Required] annotation to DTO

## DELETE endpoint

* Add DELETE method to interface and implement method in service
* Check for existing entity before deleting
* Add DELETE method to Controller