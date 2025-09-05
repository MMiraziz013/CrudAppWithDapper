# CRUD App with Dapper

**This project is a simple .NET Core console application that demonstrates CRUD (Create, Read, Update, Delete)
operations using Dapper, a lightweight and fast Object-Relational Mapper (ORM). The application interacts with a 
PostgreSQL database, with all database logic encapsulated within dedicated service classes.**

----
# 🎯 Goal

* ### The primary goal of this project is to showcase a well-structured .NET Core Console App where:

* ### All database-related logic for CRUD operations is organized in a Services/ folder.

* ### Dapper is used for efficient data access.

* ### The console application provides a simple command-line interface for user interaction.
---
# 📦 Project Structure
## The project follows a clean and logical structure to separate concerns:
```
/CrudApp
├── Program.cs                     // Main application entry point, menu, and user interaction logic
├── appsettings.json               // Configuration file for storing the database connection string
│
├── Entities/                      // Defines the C# models that map to database tables
│   ├── Company.cs
│   ├── Branch.cs
│   ├── Department.cs
│   └── Employee.cs
│
├── Services/                      // Contains the business logic and database access methods
│   ├── DbContext.cs               // Manages the Dapper database connection
│   ├── CompanyService.cs          // Handles CRUD operations for the companies table
│   ├── BranchService.cs           // Handles CRUD operations for the branches table
│   ├── DepartmentService.cs       // Handles CRUD operations for the departments table
│   └── EmployeeService.cs         // Handles CRUD operations for the employees table
```
----
# ✅ Requirements
## 1. Setup Prerequisites:

* NET 8 SDK or higher.

* A running PostgreSQL database instance.

### Steps:

1. Initialize a new .NET Core console application:
```
dotnet new console -n CrudApp
cd CrudApp
```
2. Install the required NuGet packages for Dapper and PostgreSQL:
```
dotnet add package Dapper
dotnet add package Npgsql
```
3. Configure your database connection string in appsettings.json:

```json
{
   "ConnectionStrings": {
   "DefaultConnection": "Server=localhost; Port=5432; Database=dapper1_db; User Id=postgres;"
   }
}
```

4. Set up the DbContext.cs class to provide a Dapper connection:

```csharp
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;

public class DbContext
{
private readonly string _connectionString;

    public DbContext()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);
        IConfiguration config = builder.Build();
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
```
---
## 2. Entities:
   
1. Create the following C# models in the Entities/ folder. These models will be used to map database rows to C# objects.

```
Company.cs

Branch.cs

Department.cs

Employee.cs
```
----

## 3. Services:

* Implement the full CRUD logic for each entity within its respective service class in the Services/ folder.
* Each service class should handle operations such as Add, GetById, GetAll, Update, and Delete.

* For example, the CompanyService.cs class would contain methods like AddCompany and GetAll.
----
## 4. Program.cs (Console Menu)
* The Program.cs file should contain the main application logic, including a simple command-line interface that allows the user to perform CRUD operations by selecting from a menu.
-----
## 🚀 How to Run

* Make sure your PostgreSQL database is running and the dapper1_db database exists.
* Open a terminal in the project's root directory.

* Run the application with the following command:

* dotnet run

* Follow the on-screen menu prompts to interact with the application.
