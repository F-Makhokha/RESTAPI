This sample REST API project displays music entity of the artists and their videos.

Code project implements in C# and .NET MVC using IoC/Dependency Injection design pattern.

Open solution file with Visual Studio 2012

REST API methods:

    Get a specific Artist entity
    
    Get a specific Video entity
    
    List all the videos they appear in a given artist
    
    Add/update/delete an Artist entity
    
    List all artists in the DB

Unit Test project tests following features:

    Controller test
    
    Data access test
    
    REST Web API test
    
    Follow these steps to set up the project:

Create Music.Db database: 

    Right click on SQL Server Object Explorer, create LocalDb v11.0 database called "Music.Db" Run "Database Scripts.sql" to create schema, stored procedures

Web.config & App.config: 

    Database connection: "Data Source=(localdb)\v11.0;Initial Catalog=Music.Db;Integrated Security=True"

IIS set up:

    Web API project uses IIS Express; however to allow POST/DELETE api methods work, you need to set up the Web API run on your IIS server.
