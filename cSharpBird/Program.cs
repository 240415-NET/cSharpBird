namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;

/*
## Requirements

- The application should be a C# Console Application
- The application should build and run
- The application should interact with users, and provide some console UI
- The application should allow for multiple users to log in and persist their data
- The application should demonstrate good input validation
- The application should persist data to a SQL Server DB
- The application should communicate to DB via ADO.NET or Entity Framework Core
- The application should have unit tests

## Nice to Have

- n-tier architecture
- dependency injection
- The application should log errors and system events to a file or a DB table
- Basic user authentication and authorization (admins vs normal users with passwords)
*/
class Program
{
    static void Main(string[] args)
    {
        AcctAccess.initMenu();
    }
}
