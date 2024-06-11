# AMERİKAN HASTANESİ ASSESSMENT PROJECT – NET BACKEND DEVELOPER

This project is a backend service for the blog application of the American Hospital. The project is developed using .NET 8 and PostgreSQL.

Architectural Choices

.NET 8: .NET 8 is a cross-platform framework used for developing high-performance applications. This project leverages the features and ecosystem of .NET 8.

N-Layer Architecture: This project follows an N-Layer architectural structure, consisting of the following layers. The goal is to separate different responsibilities and functions of the application to make the code more readable, maintainable, and testable.

Web API Layer: This layer handles HTTP requests and responses. It also performs request validation and authorization.
Test Layer: This layer ensures testing of controllers in the API layer.
Service Layer: This layer contains the business logic. Database operations and other backend processes are performed in this layer.
DataAccess Layer: This layer includes the context, migrations, and entity repositories. It is built using Entity Framework Core.
Entity Layer: This layer defines the tables in the application.
Core Layer: This layer forms the basic structure of the application. It contains abstract classes and the basic Generic Repository.
Common Layer: This layer includes DTOs, helpers, and attributes.
Entity Framework Core: Entity Framework Core is used for database operations. It simplifies database operations and manages the interaction between the database and the application.

Npgsql: Npgsql is used to interact with the PostgreSQL database. It is a .NET library that provides PostgreSQL database interaction.

Xunit: The Xunit testing framework is used for unit tests.

Running the Application

Clone or download the project.
Navigate to the main directory of the project in the terminal.
Run the command dotnet restore.
Run the command dotnet run --project Blog.WebApi/Blog.WebApi.csproj.
Open your browser and go to localhost:5222/swagger.
Running the Tests

Navigate to the test project directory (Blog.Test) in the terminal.
Run the command dotnet test.
API Documentation

Swagger is used for API documentation. While the application is running, you can view the API documentation by navigating to localhost:5222/swagger in your browser.