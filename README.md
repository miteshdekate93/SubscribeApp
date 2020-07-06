# SubscribeApp

Technology Uses:
Back-end: C#, Web API, code-first(CF) entity framework and MSSQL
Front-end: JavaScript/jQuery and Bootstrap

Pre-Requisite softwares to be installed:
-Visual Studio
-SQL Server and SSMS

To install and run the application follow below steps:
1). Clone with HTTP
-- copy web URL
-- Clone application using TortoiseGIT or GIT Desktop or Git Bash software
-- Go to the SubscribeApp folder
-- Double click on SubscribeApp.sln file (opens the project in Visual Studio)
-- Run program using F5 key or clicking on start button which indicates IIS Express(your default browser name) (Runs the project in your default browser)

2). Download as ZIP
-- Unzip a project
-- Go to the SubscribeApp folder
-- Double click on SubscribeApp.sln file (opens the project in Visual Studio)
-- Run program using F5 key or clicking on start button which indicates IIS Express(your default browser name) (Runs the project in your default browser)

DataBase Check:
It will auto create database under (LocalDb)\MSSQLLocalDB database server(your local database)
-- it will create SubscriberApp database and under table folder it will create dbo.SubscriberDetails table

Note: If you are running an application for the first time then the application takes some time to save data to the database because the application is using a Code-First entity framework approach which basically creates the required local database into your local machine and then then saves data.

Thank you for reading!



