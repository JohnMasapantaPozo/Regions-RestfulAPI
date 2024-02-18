Second REST API project developed suing .NET and C#. It included authorizathion, authentication, and azure deployment.

### DEV REQUIREMENTS
	1. .NET sdk and runtime 6.0 or higher.
	2. SQL Server development.
	3. SQL SSMS.

### DEPENDENCIES
```shell
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

### SUMMARY

#### Routing:
	The process of matching http request to a controller and its action method.

#### DB Context Class:
	Represents a session with the database and provides a set op API's to performa database operations.
	It funtions as a bridge between the controllers and the database.

	Responsible of:
		- Mantaining connection to DB.
		- Tracks changes to the DB.
		- Perform CRUD operations.
		- Brisge between domain models and the database.
		- Helps to create and manage database schemas.

#### Dependency Injection
	Design pattern used to increase maintinability and testability of applications by reducing decoupling between components.
	It is built into ASP.NET Core.
	DI container is responsible for creating and managing instances.

	In our context we will use DI in order to register any service instance/object we want to use across the application
	so that only one instance of such service is created an used. This way we only have to mantain the service implemenattion
	at one place and not in all the controllers or other pieces of the application that require such service.

#### Entity framework core migration to create a new database on the local SQL server.
	Performing entity framework migration is achieved by using the EntityFrameworkCore package, which is used in this case for database migrations.
		- Add-Migration: Scafolds a new migration. This analyzes the differences between our Entity clases and the previous state stored in the migration history
		and generates a migration file containing all the necesssary changes to bring the database schema in sync with our code.
		- Applies any pending migrations to the database: Reads all the migration history and determines the not yet applied migrations and executes the needed SQL
		commands to apply those migrations.

		Usefuleness:
			- When one needs to make changes/update to the database schema.
			- To keep our code in sync with the database schema, ensuring the application works well with the underlying data structure.
			- Provide version control fro the database schema.
			- Rollback changes in case a new migration caused issues.
			- Aid code-first development useful to update and manage databases without writting SQL.
			- DB migrations can be included as part od a CI/CD deployment process.
		See:
			- Django Migrations (Python)
			- Liquibase (Java, SQL)
			- Flyway (Java, .NET, and more)
			- Knex.js (Node.js, JavaScript)
		How it work:
			- First created: RestfulDbContextAModelSnapshot.cs -> contains a snapshot of the model at the time the migration is created.
			- Secondly created: "date".Migration.cs -> contains instructions for creating the initial database schema based on the model snapshot.

```shell
// EntityFrameworkCore migration

Add-Migration "Initial Migration"
Update-Database
```

#### Controllers and Actions for CRUD operations and Async Programming

#### Repository Pattern?
	It is a desing patterns to separate the data access layer from the application.
	It provides/exposses interface wihtout exposing the implementation.
	Helps create abtractions, decoupling, consistency, and multiple data sources without affecting the application logic.

	** The best practice is to inject the db context class (RestfulDbContextA) into a Repository Class which is then injected into the controller(s),
	instead of having the db context class direclty injected into the controler(s).
	
	** In this way we decouple the controllers from the db context which frees the controllers implementation from the kind of database that is used in
	the db context class. The controller will now only be aware of the "Interface" of the repository class which could now hold connection to ANY kind of
	database, and not exclusively an SLQ server as we have currently.

#### Automapper
	Object to object mapper
	Map between DTOs and Domain Models and vice-versa.

