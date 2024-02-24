# Dotnet: WebApi-Workshop
Dotnet-WebApi-Workshop: Bootstrap a basic backend in .net 5 with entity framework.

## Learned
 - Basics of general asp.net core application development
 - Basics of general sdk development for a web-api

## Prerequisites
 - .net 5.0
 - running postgres-database (docker installation showed down below)

## Getting Started

 - Install mock-database
 ```
    $ docker run -p 5432:5432 \
        --name some-postgres \
        -e POSTGRES_PASSWORD=Florian1234 \
        -d \
        --rm \
        postgres
 ```

 - Clone the repository

 ```
    $ git clone git@github.com:FlorianTh2/dotnet5BackendProject.git`
 ```

 - Switch to directory
 
 ```
    $ cd ./dotnet5BackendProject
 ```

 - Install of all dependencies
 
 ```
    $ dotnet restore
 ```

 - Run application
 
 ```
   $ dotnet run --project .\dotnet5BackendProject
 ```

 - Open swagger
 
 ```
    https://localhost:5001/swagger/index.html
 ``` 
 
 

## Important commands
 - [optional] Add support for redis (needs minor flag-setting-changes)
 
 ```
    $ docker run -p 6379:6379 redis
 ```

 - Run tests
  
 ```
    dotnet test
 ```

## Build with
 - .net 5
 - entity framework 5
 - automapper
 - postgres
 - xunit
 - swagger (openApi)
 - refit
 - fluentAssertions
 - secret manager
 - jetbrains rider
 

## Acknowledgements
Thanks [Nick Chapsas] for the awesome series about ASP.NET Core REST API. The many guides and hints were great and helped me alot.

[Nick Chapsas]: https://www.youtube.com/user/ElfocrashDev
