# Implemented APIs

## [Post]: Checklists/CreateToday
- Used to instantiate a checklist that defaults to todays date. Takes:
## [Post]: Checklists/CreateToday
- Used to instantiate a checklist that defaults to todays date. Takes:
    - (Guid) userId
    - (string) locationName

## [Post]: Checklists/Create
- Used to instantiate a checklist with any given date. Takes:
## [Post]: Checklists/Create
- Used to instantiate a checklist with any given date. Takes:
    - (Guid) userId
    - (string) locationName
    - (DateTime) checklistDateTime

## [Get]: Checklists/ListChecklist/{userId}
- Returns a list of checklists for a given userId.
## [Get]: Checklists/ListChecklist/{userId}
- Returns a list of checklists for a given userId.
    - (Guid) userId

## [Get]: Checklists/ListBirds
- Provides a list of birds for a given checklistID Guid
    - (Guid) checklistID
## [Get]: Checklists/ListBirds
- Provides a list of birds for a given checklistID Guid
    - (Guid) checklistID

## [Post]: Users/Create
- Used to create a user item with the UserCreate DTO. Takes:
## [Post]: Users/Create
- Used to create a user item with the UserCreate DTO. Takes:
    - (string) userEmail
    - (string) displayName
    - (string) rawPassword

## [Get]: Users/{usernameToFind}
- Returns a user object based on the userName (email) provided
## [Get]: Users/{usernameToFind}
- Returns a user object based on the userName (email) provided
    - (string) usernameToFind

## [Get]: Users/SignIn
- Used to sign in to the program using userName (email) and password.
## [Get]: Users/SignIn
- Used to sign in to the program using userName (email) and password.
    - (string) userName
    - (string) rawPassword

## [Post]: Users/UpdateUser (Should this be patch instead?)
- Given a Guid for userId, will update the information for a user.
## [Patch]: Users/UpdateUser
- Given a Guid for userId, will update the information for a user.
    - (Guid) userId
    - (string) email -- functionally, username
    - (string) username -- functionally displayName
    - (string) rawPassword
    - May want to fix the names for this

## [Put]: Birds/AddBird
- Adds a bird to the checklist.
## [Put]: Birds/AddBird
- Adds a bird to the checklist.
    - (Guid) checklistId
    - (string) speciesName
    - (int?) numSeen -- this MUST be converted explicitly or VS Code has a fit

## [Patch]: Birds/ListChange
- Updates a bird on the checklist
## [Patch]: Birds/ListChange
- Updates a bird on the checklist
    - (Guid) checklistId
    - (string) speciesName
    - (int?) numSeen -- this MUST be converted explicitly or VS Code has a fit

# Pending Implementation

## [Delete] Checklist
## [Delete] Bird(s)

