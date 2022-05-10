# MyYonderEvents

## Backend WebAPI Setup

### Generate database and update to latest migration:

* ` dotnet ef database update `

### Build and run:

* ` dotnet build `
* ` dotnet run `
* ` dotnet run watch ` - for auto reload on changes
* Swagger: ` https://localhost:5001/swagger/index.html `

### Additional resources:

* [.NET documentation | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/fundamentals/)
* [Overview of Entity Framework Core - EF Core | Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/)
* [Use Code First Migrations to Seed the Database | Microsoft Docs](https://docs.microsoft.com/en-gb/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-3)
* [Configure One-to-One relationship in Code First Entity Framework ](https://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx)
* [Get Started with ASP.NET Web API 2 (C#) - ASP.NET 4.x | Microsoft Docs](https://docs.microsoft.com/en-gb/aspnet/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api)
* [C# Coding Conventions | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## Frontend Setup

### Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

### Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

### Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
