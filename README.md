# README - RESTFul API Development

REST API developed project usuing .NET and C#. The API is designed using repository and dependency injection
design patterns in order to let the API be migrated with ease to other databases if required without affecting
its funtionality nor demand extensive modifications as this decouples the API funtionality from the database
operations.

It includes CRUD operations, authorizathion, authentication, and azure deployment.
A simple .NET web application to interact with the REST API which is not the main purpose of the current project.

This is a summary of the most important features. See the internal README.md files for additiional information.

- Routing helps match HTTP requests to a controller and its action method.
- The DbContext class represents a session with the database and provides an API for database operations.
- Dependency Injection is a design pattern to increase maintainability and testability of applications.
- Entity Framework Core Migration helps to make updates and changes to the database schema.
- Repository Pattern helps decouple the data access layer from the application and provides an abstraction layer.
- AutoMapper maps between DTOs and Domain Models.
- Seed data and apply migrations to ensure our code is in-sync with the database schema.
- Navigation Properties allow us to navigate between entities in a database.
- Token-based Authentication and Role-based Authorization protect resources from unauthorized access and grant specific access.
- Image Upload Functionality allows us to upload images and serve static images with a preconstructed https URL.
- Logging with Serilog helps us optimize our code with easy-to-read logs.
- Global Exception Handling offers consistency in handling exceptions.
- API Versioning with URL-based versioning allows us to version our API and maintain backward compatibility.
- The REST API can be deployed to Azure using Azure Settings.json.

## DEV REQUIREMENTS
	1. .NET sdk and runtime 6.0 or higher.
	2. SQL Server development.
	3. SQL SSMS.

## DEPENDENCIES
```shell
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```
