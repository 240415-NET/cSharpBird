# Implemented APIs

## [Post]
- Checklists/CreateToday
    - (Guid) userId
    - (string) locationName

## [Post]
- Checklists/Create
    - (Guid) userId
    - (string) locationName
    - (DateTime) checklistDateTime

## [Get]
- Checklists/ListChecklist
    - (Guid) userId

## [Get]
- Checklists/ListBirds
    - (Guid) checklistId

## [Post]
- Users/Create
    - (string) userEmail
    - (string) displayName
    - (string) rawPassword

## [Get]
- /Users/{usernameToFind}
    - (string) usernameToFind

## [Get]
- /Users/SignIn
    - (string) userName
    - (string) rawPassword

## [Post]
- /Users/UpdateUser
    - (Guid) userId
    - (string) email -- functionally, username
    - (string) username -- functionally displayName
    - (string) rawPassword
    - May want to fix the names for this

## [Put]
- /Birds/AddBird
    - (Guid) checklistId
    - (string) speciesName
    - (int?) numSeen -- this MUST be converted explicitly or VS Code has a fit

## [Patch]
- /Birds/ListChange
    - (Guid) checklistId
    - (string) speciesName
    - (int?) numSeen -- this MUST be converted explicitly or VS Code has a fit

# Pending Implementation

## [Delete] Checklist
## [Delete] Bird(s)

