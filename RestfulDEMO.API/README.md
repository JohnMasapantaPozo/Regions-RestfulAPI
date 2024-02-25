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


#### Seed data to the database and apply migration
	Adding some data to the database from the db context class and apply a migration to get it ingested to the db.
	Open package manage console:
		- Add-Migration "Seeding data for difficutlies and regions"
		- Update-Database

#### Navigation properties
	Allow to navigate from one entity to another in a database.
	Represent a relationship between entitites.
	This will alllow return to the client data in related entities.

#### Authoziation and authentication
	Authentication identifies the user using hir/her credential while authorization determines what kind of permisions a certain user has over the resources attempted to access.
	
	Stateless APIs often use token-based authentication mechanisms like JSON Web Tokens (JWT), OAuth, and others.

	1. Auithentication Flow (JWT - JSON Web Token)
	User login credentials -> API -> JWT Token -> Http calls (JWT Token) -> API checks JWT Token -> Returns the resources if succeds.

	Dependencies:
		- Microsoft.AspNetCore.Authentication.JwtBearer == 7.0.4
		- Microsoft.IdentityModel.Tokens == 6.27.0
		- System.IdentityModel.Tokens.Jwt == 6.27.0
		- Microsoft.AspNetCore.Identity.EntityFrameworkCore == 7.0.4

	2. Now it time to set up Authentication for our database
		- Update connection string: RestFulDEMOAuthConnectionString
		- Update Db context with roles (seed some users to the database): RestFulAuthDbContext
		- Inject udated DB context and Identity: RestFulAuthDbContext
		- Entitiy Framework migration.
			Comands:
			Add-Migration "Creating Auth Database" -Context "RestFulAuthDbContext"
			Update-Database -Context "RestFulAuthDbContext"

	3. Inject Identity dependencies

	4. Auth Controller
		Once we have injested identity as a service to our program.cs, we then need an Auth controller
		to check register a new user in our Auth DB and to check for a user credentials when he/she
		authenticates agains the api.

	5. TokenRepository : ItokenRepository
		A repository to handle the token creation/retrieval detached from the Auth Controller.

	6. Implement Role based authorization on the Controllers
		The [Authorize] decorator in the RegionsController protects all the routes for that specific controller but it
		does not take into account the type of role the user attempting to acess the resources has.

		To add role base authorization, each route should require its independent and specific type of role authorization
		in order to be interacted with.

	7. Enable the authorization featyure for swagger

#### Add image upload functionality
	Add new Dto to the DB context and migrate the DB.
	Commands:
		- Add-Migration "Add Images table" -Context "RestfulDbContextA"
		- Update-Database -Context "RestfulDbContextA"

	Add a LocalImageRepository:IImageRepository and its respective ImmageController.
	The image local repository will construct an https file path that will be used to serve the locally strored images.
	Then the UseStaticFiles method in the api middleware pipeline will hel us make our static images to be served using the previously constructed https url.