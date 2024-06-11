# AMERİKAN HASTANESİ ASSESSMENT PROJECT – NET BACKEND DEVELOPER

This project is a backend service for the blog application of the American Hospital. The project is developed using .NET 8 and PostgreSQL.

## Introduction

The purpose of this project is to provide a robust and scalable backend service for a blog application. The application is designed to handle various functionalities such as user authentication, post management, and comments.

## Architectural Choices

- **.NET 8:**
    - Chosen for its cross-platform capabilities, high performance, and extensive ecosystem.
    - Enables the development of modern, scalable, and maintainable applications.

- **N-Layer Architecture:**
    - **Web API Layer:** Handles HTTP requests and responses, validates and authorizes incoming requests.
    - **Test Layer:** Ensures the functionality of controllers through unit tests.
    - **Service Layer:** Contains business logic and handles backend operations and database interactions.
    - **DataAccess Layer:** Manages the database context, migrations, and repositories using Entity Framework Core.
    - **Entity Layer:** Defines the database tables.
    - **Core Layer:** Contains fundamental structures such as abstract classes and generic repositories.
    - **Common Layer:** Includes DTOs, helpers, and attributes for the application.

## Technologies Used

- **Entity Framework Core:**
    - Simplifies database operations and manages the interaction between the application and the database.
    - Provides an ORM (Object-Relational Mapping) framework for .NET applications.

- **Npgsql:**
    - A .NET library used to interact with PostgreSQL databases.
    - Facilitates seamless communication between .NET applications and PostgreSQL.

- **Xunit:**
    - A testing framework for .NET applications.
    - Used for writing unit tests to ensure the application functions as expected.

## Project Structure

- **Blog.WebApi:** The entry point for the web API, handles HTTP requests and responses.
- **Blog.Test:** Contains unit tests for the application.
- **Blog.Service:** Implements the business logic and interacts with the DataAccess layer.
- **Blog.DataAccess:** Manages database context, migrations, and repositories.
- **Blog.Entity:** Defines the entities representing the database tables.
- **Blog.Core:** Contains core functionalities, abstract classes, and generic repositories.
- **Blog.Common:** Includes common utilities, DTOs, helpers, and attributes.

## Running the Application

- Clone or download the project.
- Navigate to the main directory of the project in the terminal.
- Run the command dotnet restore.
- Run the command dotnet run --project Blog.WebApi/Blog.WebApi.csproj.
- Open your browser and go to `localhost:5222/swagger`.

## Running the Tests

- Navigate to the test project directory (Blog.Test) in the terminal.
- Run the command dotnet test.

## API Documentation

Swagger is used for API documentation. While the application is running, you can view the API documentation by navigating to `localhost:5222/swagger` in your browser.