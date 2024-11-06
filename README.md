# VINTAGE SHOPPING PLATFORM
This project is an ASP.NET Core Web API application designed for an online shopping platform. It implements layered architecture, authentication, authorization, custom middleware, data protection, and other modern software development principles.

A place to buy vintage items or clothes 

## Project Structure and Requirements
### Layered Architecture
The project follows a layered architecture with three main layers:

### Presentation Layer (API Layer): 
Contains API controllers.
### Business Layer:
Contains business logic services.
### Data Access Layer:
Manages database operations using Entity Framework Code First and includes repository and UnitOfWork classes.

## Authentication and Authorization
Custom authentication service is used for user authentication.
JWT Token is used for authorization.
Role-based authorization checks (e.g., "Customer", "Admin") are implemented.

## Middleware
Three custom middleware components are implemented:
### RequestLoggingMiddleware: 
Logs the URL, request time, and user ID of each incoming request.
### MaintenanceMiddleware: 
Enables maintenance mode by checking a maintenance status table
### ExceptionHandlingMiddleware:
Global Exception Handling in this project is used to catch and manage all unhandled exceptions in a centralized manner. This setup ensures consistent error responses and efficient logging, improving the overall robustness and maintainability of the application.

## Action Filter
A custom Action Filter restricts access to certain API actions based on specified time intervals, allowing timed access control.

## Dependency Injection
Services are managed via Dependency Injection (DI).

## Model Validation
Validation rules are applied for the User and Product models, covering requirements like email format, required fields checks.

## Data Protection
User passwords are securely stored using Data Protection.

## Setup and Run Instructions
Requirements

.NET 7.0 SDK
SQL Server or compatible database server

Installation Steps
Update the database connection settings in appsettings.json in the project directory.
Run the following commands in the project root directory to set up and launch the application:

```
dotnet restore
dotnet ef database update
dotnet run
The application will be accessible at http address by default.
```

## Technologies Used
ASP.NET Core Web API
Entity Framework Core (Code First)
ASP.NET Core Identity & JWT Authentication
Data Protection API
Action Filters and Middleware
Dependency Injection (DI)

## Contributing
Please submit a pull request or open an issue if you'd like to contribute or suggest improvements.
