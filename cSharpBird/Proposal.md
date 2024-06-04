## Proposal
- There must be a way to store checklists while eBird is down for maintenance or issues

## Requirements

- The application should be a C# Console Application: **DONE**
- The application should build and run: **DONE**
- The application should interact with users, and provide some console UI: **DONE** Common UI elements are defined to streamline implementation too.
    - Single user type that has objects associated with it (checklists): **DONE**
    - ~~Superuser that controls additional classes of objects (locations, species)~~
- The application should allow for multiple users to log in and persist their data: Mostly DONE
    - Checklists will be saved in SQL ~~/JSON~~; option to print as text file for user to refer to later: **DONE** (JSON version, optional print to text file)
- The application should demonstrate good input validation: **DONE**
    - Validation of ~~locations and~~ species: **DONE**
    - Validation of email format: **DONE**
- The application should persist data to a SQL Server DB
    - Checlists, ~~birds~~ and users will be stored here
    - Birds will be stored in CSV file for easy update based on USGS list
- The application should communicate to DB via ADO.NET or Entity Framework Core


## Nice to Have

- n-tier architecture: **DONE**
    - Presentation
    - Controller
    - Interfaces
    - Data Access
    - Common UI Elements
- dependency injection
- The application should log errors and system events to a file or a DB table
- Basic user authentication and authorization (admins vs normal users with passwords): **DONE**
- The application should have unit tests: **DONE**

## Individual Requirements
- User Object
    - User object should possess:
        - Unique identifier (GUID; assigned at creation)
        - Unique email (string; checked against database)
        - Name (string; can be added or changed after account creation, nullable)
        - Password
    - Will be related to other objects by:
        - Checklist:
            - "Owner" of checklist, able to edit and print
- Checklist Object
    - Checklist object should possess:
        - Unique identifier (GUID; assigned at creation)
        - Birder (object; assigned at creation)
        - Location (string/object; assigned at creation by user)
        - Date (DateTime; assigned at creation by user OR system)
        - List<Bird> (list of objects; assigned AFTER creation by Birder) || ~~List<List <string>>> (list of banding codes, number as string)~~
        - Checklist Duration (float; assigned at creation)
        - Checklist Distance (float; assigned at creation)
    - Will be related to other objects by:
        - Birder/Owner able to edit and print after log in
        - Location, containing details of given hotspot (optional)
        - Birds, by containing list of birds seen
- Bird Object (Nice to have; otherwise, pull from CSV/SQL)
    - Bird Object should possess:
        - ~~Unique identifier (GUID; assigned at creation)~~
        - Band Code (string; assigned at creation)
        - Species name (string; assigned at creation)
        - ~~Rarity (string; assigned at creation by user OR system default)~~
        - Count (number (int?) updated by user)
    - Will be related to other objects by:
        - Contained within a List in checklist
- ~~Location Object (Nice to have; otherwise, just userdata)~~
    - ~~Location Object should possess:~~
        - ~~Unique identifier (GUID; assigned at creation)~~
        - ~~Location name (string; assigned at creation)~~
        - ~~County (string; assigned at creation by user OR system default)~~
        - ~~State (string; assigned at creation by user OR system default)~~

