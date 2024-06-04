# Project 2

## Requirements (Presenting on: ~6/20/24)

- The application should be an ASP.NET REST-ful API
- The application should build and run
- The application should provide user interaction via an HTML/CSS/JS front end.
- The application should allow for multiple users to log in and persist their data (CRUD operations.)
- The application should persist data to a SQL Server DB
-- Currently persists with ADO.NET; will create branch to try and migrate to EF Core (existing interface should ease)
- The application should communicate to DB via ADO.NET or Entity Framework Core
-- EF Core migration is planned
- Verify functionality via Swagger during project presentations

## Nice to Have

- Provide API documentation (routes, schemas, etc)
- The application should log errors and system events to a file or a DB table
- User authorization (admins vs normal users with passwords)
-- Done in existing format (but no admin accounts)
-- Creation of admin account with full permissions is possible
- The application should have unit tests
-- Unit tests exist, but deprecated tests need to be removed
- Session management
