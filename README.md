# Policy Notes API & Tests

## API Endpoints 

➤ POST ```/notes```
Create a new policy note
Response: 201 Created

➤ GET ```/notes```
Get all notes
Response: 200 OK

➤ GET ```/notes/{id}```
Get a single note
200 OK when found
404 NotFound when missing

## Project Structure

```text
PolicyNotesApi/
│   PolicyNotesApi.sln
│
├── PolicyNotes
│   ├── Program.cs
│   ├── PolicyNotes.csproj
│   │
│   ├── Data/
│   │     NotesDbContext.cs
│   │
│   ├── Models/
│   │     PolicyNote.cs
│   │
│   ├── Repositories/
│   │     IPolicyNoteRepository.cs
│   │     PolicyNoteRepository.cs
│   │
│   └── Services/
│         PolicyNoteService.cs
│
└── PolicyNotes.Tests
    ├── PolicyNotes.Tests.csproj
    │
    ├── UnitTests/
    │     PolicyNotesUnitTests.cs
    │
    └── IntegrationTests/
          PolicyNotesIntegrationTests.cs
```

### 🧪 Testing

This project includes:

✔ Unit Tests

Located in [UnitTests](./PolicyNotes.Tests/UnitTests)

Tests the service layer:
-Adding a note
-Retrieving notes

✔ Integration Tests

Located in [IntegrationTests](./PolicyNotes.Tests/IntegrationTests)
Using WebApplicationFactory<Program>

Tests API endpoints:

-POST /notes → 201 Created
-GET /notes → 200 OK
-GET /notes/{id} → 200 OK / 404 NotFound

✔ Code Coverage

Generated using:

```dotnet test --collect:"XPlat Code Coverage"```


Coverage report located in:

[CoverageResults](PolicyNotes.Tests/TestResults/)


Achieved 100% coverage !
