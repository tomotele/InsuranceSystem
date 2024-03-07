# InsuranceSystem
Health Insurance Enterprise Resource Planning (ERP) system. 
This system is responsible for managing health insurance
policies, processing claims, and interacting with Hospital Electronic Health Records (EHRs) for
efficient claim processing. 
## Getting Started
Before you begin, ensure you have the following installed:

.NET 6 SDK

Visual Studio Code or Visual Studio 2022

MSSQL

Docker Desktop

## Here's a step-by-step guide for running the code
1 Clone the Repository: Clone the application repository to your local machine using Git. 

2 Restore NuGet Packages: Once the repository is cloned, open the solution file (.sln) in Visual Studio 2022. Right-click on the solution in Solution Explorer and select "Restore NuGet Packages". This will download and restore all required NuGet packages for the project.

3 Set up Database Connection: Open the appsettings.json file and update the connection string under "ConnectionStrings" with your local SQL Server instance details. Ensure that you have SQL Server installed and running on your machine.

4 Run Migrations: Open the Package Manager Console in Visual Studio 2022 (View -> Other Windows -> Package Manager Console). 
Run the following commands to apply migrations and update the database:
```
Add-Migration InitialCreate
```
```
Update-Database
```

5 Building The Docker Images: To build the images, make sure you have cloned the project to your machine, then run the following;

right click on the docker compose project file and then click on open in terminal to run the following command

```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
5 Run the Application: Build the solution in Visual Studio 2022, and then press F5 or click on the "Start" button to run the application. The application should now be up and running, and you can access it through your web browser or testing tool of choice.

## Technologies Used

The following technologies and libraries were used in this project:

- Entity Framework Core: ORM (Object-Relational Mapping) framework for database interaction.
  
- AutoMapper: Library for object-to-object mapping.
  
- AutoFixture: Library for generating test data and objects.
  
- Moq: Mocking library for unit testing.
  
- xUnit: Testing framework for unit tests.
  
- Serilog: Logging framework for structured logging.
  
- Marvin.Cache.Headers: Library for adding caching headers to HTTP responses.

- Docker/Docker Compose: Used for Containerization
