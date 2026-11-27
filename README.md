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

The student clarified that the React website is sufficient as the user client for grade 5. A separate mobile application is outside this project scope.

## Status

The GitHub Project has 27 tasks in the current scope, with labels and due dates. Dropped tasks are kept in issue history. The application is not implemented yet.

- [GitHub Project board](https://github.com/users/Testik207/projects/3/views/1)
- [Development plan](docs/planning/PLAN.md)
- [Assignment checklist](docs/planning/REQUIREMENTS.md)
- [Task list for GitHub](docs/planning/BACKLOG.md)

## Workflow

Create the GitHub repository, Project board and at least 20 issues before application development. Each issue has labels, a due date and acceptance criteria.

Use a branch such as `feature/7-authentication` for an issue. Make commits after working changes, open a pull request and link it to the issue. Add `Closes #7` to the pull request when all acceptance criteria are met. Keep the individual commits when merging.

Commit on each day spent working on the project. Record actual progress; unfinished tasks stay open.

## Development setup

The selected stack is C#, .NET 10, ASP.NET Core, EF Core with the Npgsql PostgreSQL provider, and React with JavaScript. The editor is VS Code. A managed PostgreSQL database keeps the development data independent of either PC. The assignment does not prescribe a database provider.

The student's development machine is Windows with VS Code. Tested SDK versions and startup commands will be added with the application scaffold. Credentials will use local configuration rather than committed passwords.

## Submission

- GitHub repository link.
- Documentation in Estonian, exported to PDF using the school's formatting guide.
- Project demonstration and explanation in class.

The submission deadline is 4 December 2026. The school documentation formatting guide still needs to be supplied.
