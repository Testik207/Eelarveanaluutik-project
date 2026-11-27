# Eelarveanalüütik

A personal budget application for the Programming and Distributed Applications final project.

The user records income and expenses, imports transactions from CSV, sets a monthly budget and views spending summaries.

## Planned applications

| Application | Purpose |
| --- | --- |
| ASP.NET Core Web API | Authentication, business rules and database access |
| React web client | Transactions, imports, budgets and charts |
| .NET Worker Service | CSV processing and monthly report scheduling through the API |

Only the API accesses the database. The React website and Worker use HTTP. The web client receives import updates through an authenticated WebSocket.

## Status

The GitHub Project has 27 tasks in the current scope, with labels and due dates. Dropped tasks are kept in issue history. The initial scaffold includes an API status endpoint, a React connection check and a separate Worker process. Budget features are still planned.

- [GitHub Project board](https://github.com/users/Testik207/projects/3/views/1)
- [Development plan](docs/planning/PLAN.md)
- [Assignment checklist](docs/planning/REQUIREMENTS.md)
- [Task list for GitHub](docs/planning/BACKLOG.md)

## Workflow

Create the GitHub repository, Project board and at least 20 issues before application development. Each issue has labels, a due date and acceptance criteria.

Use a branch such as `feature/7-authentication` for an issue. Make commits after working changes, open a pull request and link it to the issue. Add `Closes #7` to the pull request when all acceptance criteria are met. Keep the individual commits when merging.

Commit on each day spent working on the project. Record actual progress; unfinished tasks stay open.

## Development setup

The stack is C#, .NET 10, ASP.NET Core, EF Core with the Npgsql PostgreSQL provider, and React with JavaScript. The editor is VS Code. PostgreSQL configuration will be added in T28; this scaffold starts without a database.

Install the .NET 10 SDK and a supported Node.js version (22.12+ or 24+). Checked with SDK 10.0.404 and Node.js 24.22.3 on Linux. The commands below use PowerShell in VS Code.

### Project structure

| Path | Purpose |
| --- | --- |
| `src/Eelarveanaluutik.Api` | HTTP API and controllers |
| `src/Eelarveanaluutik.Contracts` | Shared API DTOs |
| `src/Eelarveanaluutik.Worker` | Separate background process |
| `tests/Eelarveanaluutik.Tests` | xUnit project for the upcoming service tests |
| `web` | React JavaScript frontend built with Vite |

### Restore and build

Run from the repository root:

```powershell
dotnet restore Eelarveanaluutik.sln
dotnet build Eelarveanaluutik.sln --no-restore
```

### Start the API

In the first terminal, from the repository root:

```powershell
dotnet run --project src/Eelarveanaluutik.Api --launch-profile http
```

Check `http://localhost:5151/api/health`. The response should contain `status: "ok"` and the API name. This endpoint only checks that the server is running.

### Start React

Open a second terminal:

```powershell
cd web
npm.cmd ci
npm.cmd run dev
```

Open `http://127.0.0.1:5173`. The page checks the API connection and has a retry button. Vite forwards `/api` requests to `http://localhost:5151` during development.

On macOS or Linux, use `npm` in place of `npm.cmd`.

### Start the Worker

In a third terminal, from the repository root:

```powershell
dotnet run --project src/Eelarveanaluutik.Worker
```

At this stage, the Worker logs that it has started and waits until stopped. CSV processing and monthly reports will be added in T19 and T21. Stop each running process with Ctrl+C.

### Frontend checks

From `web`:

```powershell
npm.cmd run lint
npm.cmd run build
```

### Tests

The xUnit project is configured, but the generated empty test has been removed. Real tests will be added with the business logic. The assignment requires at least eight unit tests, including two with mocks.

Run the tests after they have been added:

```powershell
dotnet test Eelarveanaluutik.sln
```
