# GitHub task backlog

Submission deadline: 4 December 2026. The 29 tasks below include the React frontend, shared PostgreSQL setup and the school-PC workflow. Issue links are filled after creation.

| Task | Part | Title | Labels | Due date |
| --- | --- | --- | --- | --- |
| T01 | 1 | Create the repository and project README | dokumentatsioon | 2026-11-27 |
| T02 | 1 | Set up the GitHub Project and development tasks | dokumentatsioon | 2026-11-27 |
| T03 | 1 | Create the solution and application scaffold | backend, frontend, taustateenus | 2026-11-27 |
| T04 | 2 | Define the budget entities and relationships | backend, andmebaas | 2026-11-28 |
| T05 | 2 | Add EF Core migrations and seed data | andmebaas | 2026-11-28 |
| T06 | 2 | Add repositories and register application services | backend, andmebaas | 2026-11-28 |
| T07 | 3 | Add JWT login and User Admin Service roles | backend, api, autentimine | 2026-11-28 |
| T08 | 3 | Add DTO validation and consistent API errors | backend, api | 2026-11-28 |
| T09 | 3 | Document API requests in Swagger and an HTTP file | api, dokumentatsioon | 2026-11-28 |
| T10 | 4 | Add transaction and category CRUD endpoints | backend, api | 2026-11-29 |
| T11 | 4 | Add transaction search filters sorting and pagination | backend, api | 2026-11-29 |
| T12 | 4 | Calculate monthly income expenses and balance | backend, api | 2026-11-29 |
| T13 | 5 | Build React login and registration screens | frontend, autentimine | 2026-11-30 |
| T14 | 5 | Build React transaction management screens | frontend | 2026-11-30 |
| T15 | 5 | Add monthly budgets and the summary dashboard | backend, frontend, api | 2026-11-30 |
| T16 | 6 | Add automatic category rules and their management screen | backend, frontend, api | 2026-12-01 |
| T17 | 6 | Accept CSV uploads and expose import status | backend, frontend, api | 2026-12-01 |
| T18 | 6 | Process queued import rows through batch endpoints | backend, api | 2026-12-01 |
| T19 | 7 | Connect the Worker to the import API | backend, taustateenus, api | 2026-12-02 |
| T20 | 7 | Show authenticated live import progress | backend, frontend, reaalajas | 2026-12-02 |
| T21 | 7 | Create monthly reports from the Worker schedule | backend, taustateenus, api | 2026-12-02 |
| T22 | 8 | Add expense distribution and history charts | frontend | 2026-12-03 |
| T23 | 8 | Add an explainable next-month expense forecast | backend, frontend | 2026-12-03 |
| T24 | 8 | Build the MAUI client with MVVM | mobiil, frontend, autentimine | 2026-12-03 |
| T25 | 9 | Complete the required unit test coverage | testid, backend | 2026-12-04 |
| T26 | 9 | Verify the complete application and fix integration issues | testid | 2026-12-04 |
| T27 | 9 | Prepare the Estonian report and project demonstration | dokumentatsioon | 2026-12-04 |
| T28 | 2 | Configure a shared PostgreSQL database | andmebaas, backend | 2026-11-28 |
| T29 | 5 | Document setup after the school PC is cleared | dokumentatsioon, testid | 2026-11-30 |

## T01: Create the repository and project README

Labels: dokumentatsioon. Due: 2026-11-27.

Acceptance criteria:

- [ ] The GitHub repository is accessible to the student.
- [ ] The English README describes the application and planned components.
- [ ] Git ignores build output, credentials and local database files.

Commit steps:

1. Add the English README.
2. Add Git ignore rules and editor settings.

GitHub issue: [#1](https://github.com/Testik207/Eelarveanaluutik-project/issues/1).

## T02: Set up the GitHub Project and development tasks

Labels: dokumentatsioon. Due: 2026-11-27.

Acceptance criteria:

- [ ] The Project has Todo, In progress and Done statuses.
- [ ] At least 20 issues have labels, explicit due dates and acceptance criteria.
- [ ] Issues are added to the Project and reference their planned task IDs.

Commit steps:

1. Add the requirements checklist and development plan.
2. Record the issue list, due dates and GitHub workflow templates.

GitHub issue: [#2](https://github.com/Testik207/Eelarveanaluutik-project/issues/2).

## T03: Create the solution and application scaffold

Labels: backend, frontend, taustateenus. Due: 2026-11-27.

Acceptance criteria:

- [ ] The .NET solution contains API, shared contracts, Worker and test projects with clear responsibilities.
- [ ] The React JavaScript frontend is separate from the API and uses it over HTTP.
- [ ] The README gives tested commands for Windows and VS Code; mobile setup is documented separately.

Commit steps:

1. Create the .NET solution and API, contracts, Worker and test projects.
2. Create the React JavaScript frontend.
3. Document and check the startup commands.

GitHub issue: [#3](https://github.com/Testik207/Eelarveanaluutik-project/issues/3).

## T04: Define the budget entities and relationships

Labels: backend, andmebaas. Due: 2026-11-28.

Acceptance criteria:

- [ ] The model has at least five related tables.
- [ ] Transactions, budgets, rules and imports belong to a user.
- [ ] Amounts use decimal values and transaction dates have a defined format.

Commit steps:

1. Add users, categories and transaction models.
2. Add budget, category rule, import and report relationships.

GitHub issue: [#4](https://github.com/Testik207/Eelarveanaluutik-project/issues/4).

## T05: Add EF Core migrations and seed data

Labels: andmebaas. Due: 2026-11-28.

Acceptance criteria:

- [ ] EF Core migrations create the PostgreSQL schema using a configured connection.
- [ ] Synthetic categories and transactions can be seeded without duplicating existing seed records.
- [ ] The setup can be repeated on a second computer without deleting existing shared data.

Commit steps:

1. Create the first PostgreSQL migration.
2. Add repeatable synthetic seed data and setup instructions.

GitHub issue: [#5](https://github.com/Testik207/Eelarveanaluutik-project/issues/5).

## T06: Add repositories and register application services

Labels: backend, andmebaas. Due: 2026-11-28.

Acceptance criteria:

- [ ] Repositories encapsulate data access needed by the application.
- [ ] Business logic uses injected repositories and services.
- [ ] Only API-side projects reference the database implementation.

Commit steps:

1. Add repository interfaces and implementations.
2. Register the services and dependencies in the API.

GitHub issue: [#6](https://github.com/Testik207/Eelarveanaluutik-project/issues/6).

## T07: Add JWT login and User Admin Service roles

Labels: backend, api, autentimine. Due: 2026-11-28.

Acceptance criteria:

- [ ] Registration and login return suitable responses and passwords are hashed.
- [ ] User, Admin and Service access restrictions are enforced.
- [ ] JWT signing secrets and service passwords come from local configuration.

Commit steps:

1. Implement registration, password hashing and login.
2. Add JWT validation and User, Admin and Service access rules.

GitHub issue: [#7](https://github.com/Testik207/Eelarveanaluutik-project/issues/7).

## T08: Add DTO validation and consistent API errors

Labels: backend, api. Due: 2026-11-28.

Acceptance criteria:

- [ ] Public endpoints accept and return DTOs rather than database entities.
- [ ] Invalid requests produce clear validation errors.
- [ ] Unexpected errors use global ProblemDetails handling and are logged without exposing secrets.

Commit steps:

1. Define request and response DTOs and input validation.
2. Add global ProblemDetails handling and logging.

GitHub issue: [#8](https://github.com/Testik207/Eelarveanaluutik-project/issues/8).

## T09: Document API requests in Swagger and an HTTP file

Labels: api, dokumentatsioon. Due: 2026-11-28.

Acceptance criteria:

- [ ] Swagger shows endpoint descriptions, request examples and JWT Authorize.
- [ ] CORS permits the configured web-client origin.
- [ ] An HTTP request file covers authentication and is extended as endpoints are added.

Commit steps:

1. Configure Swagger descriptions, examples and Authorize.
2. Add CORS and an HTTP request file for the implemented endpoints.

GitHub issue: [#9](https://github.com/Testik207/Eelarveanaluutik-project/issues/9).

## T10: Add transaction and category CRUD endpoints

Labels: backend, api. Due: 2026-11-29.

Acceptance criteria:

- [ ] A user can create, read, edit and delete their transactions and categories.
- [ ] Income and expenses have valid amounts, dates, descriptions and categories.
- [ ] Another user's records cannot be read or changed and HTTP status codes are appropriate.

Commit steps:

1. Implement category endpoints and ownership checks.
2. Implement transaction CRUD and appropriate HTTP responses.

GitHub issue: [#10](https://github.com/Testik207/Eelarveanaluutik-project/issues/10).

## T11: Add transaction search filters sorting and pagination

Labels: backend, api. Due: 2026-11-29.

Acceptance criteria:

- [ ] GET /api/transactions supports from, to and category filters.
- [ ] Text search, supported sorting and bounded pagination work together.
- [ ] Empty results and invalid filter parameters have predictable responses.

Commit steps:

1. Add search, date and category filters.
2. Add supported sorting and bounded pagination.

GitHub issue: [#11](https://github.com/Testik207/Eelarveanaluutik-project/issues/11).

## T12: Calculate monthly income expenses and balance

Labels: backend, api. Due: 2026-11-29.

Acceptance criteria:

- [ ] The summary is scoped to the signed-in user and selected month.
- [ ] Income, expenses and balance match known example transactions.
- [ ] A month with no transactions returns zero totals.

Commit steps:

1. Implement the monthly total calculations.
2. Add the summary endpoint and calculation tests.

GitHub issue: [#12](https://github.com/Testik207/Eelarveanaluutik-project/issues/12).

## T13: Build React login and registration screens

Labels: frontend, autentimine. Due: 2026-11-30.

Acceptance criteria:

- [ ] The user can register, log in and log out through the API.
- [ ] Protected pages handle missing or expired sessions.
- [ ] Forms validate input and show useful API errors.

Commit steps:

1. Build the React registration and login forms.
2. Add session handling, logout and protected routes.

GitHub issue: [#13](https://github.com/Testik207/Eelarveanaluutik-project/issues/13).

## T14: Build React transaction management screens

Labels: frontend. Due: 2026-11-30.

Acceptance criteria:

- [ ] The site lists, adds, edits and deletes transactions using the API.
- [ ] Search, filters, sorting and pagination are available in the UI.
- [ ] Loading, empty and error states are understandable and forms validate input.

Commit steps:

1. Build the React transaction list and forms.
2. Connect search, filters, sorting and pagination to the API.

GitHub issue: [#14](https://github.com/Testik207/Eelarveanaluutik-project/issues/14).

## T15: Add monthly budgets and the summary dashboard

Labels: backend, frontend, api. Due: 2026-11-30.

Acceptance criteria:

- [ ] GET /api/budgets/{month}/summary returns the monthly totals and budget information.
- [ ] The user can set and update a monthly budget.
- [ ] The site shows remaining budget and a clear overspending warning.

Commit steps:

1. Add the budget settings and monthly summary API.
2. Build the React dashboard and overspending warning.

GitHub issue: [#15](https://github.com/Testik207/Eelarveanaluutik-project/issues/15).

## T16: Add automatic category rules and their management screen

Labels: backend, frontend, api. Due: 2026-12-01.

Acceptance criteria:

- [ ] CRUD /api/category-rules is scoped to the current user.
- [ ] The UI can add, edit and delete rules such as Rimi to Food.
- [ ] Case handling and precedence for multiple matching rules are documented and tested.

Commit steps:

1. Implement rule CRUD and matching logic.
2. Add the React rule management screen and matching tests.

GitHub issue: [#16](https://github.com/Testik207/Eelarveanaluutik-project/issues/16).

## T17: Accept CSV uploads and expose import status

Labels: backend, frontend, api. Due: 2026-12-01.

Acceptance criteria:

- [ ] A documented CSV format and a synthetic sample file are supplied.
- [ ] POST /api/imports validates an upload and returns 202 with an import ID.
- [ ] GET /api/imports/{id} returns only the owner's import status and the UI can upload a file.

Commit steps:

1. Add CSV upload validation and import status endpoints.
2. Add the upload screen and a synthetic sample file.

GitHub issue: [#17](https://github.com/Testik207/Eelarveanaluutik-project/issues/17).

## T18: Process queued import rows through batch endpoints

Labels: backend, api. Due: 2026-12-01.

Acceptance criteria:

- [ ] Service can list pending imports and process up to 100 rows per batch through the specified endpoints.
- [ ] Processing applies category rules, records progress and reports invalid rows clearly.
- [ ] Retrying a completed batch or resuming an import does not add the same stored rows twice.

Commit steps:

1. Add pending-import and batch-processing endpoints.
2. Handle invalid rows and safe retries with relevant checks.

GitHub issue: [#18](https://github.com/Testik207/Eelarveanaluutik-project/issues/18).

## T19: Connect the Worker to the import API

Labels: backend, taustateenus, api. Due: 2026-12-02.

Acceptance criteria:

- [ ] A separate BackgroundService logs in with its Service account and uses HttpClient.
- [ ] PeriodicTimer checks for pending imports every 10 seconds and processes successive batches.
- [ ] The Worker has no DbContext or database connection and handles cancellation and temporary API failures.

Commit steps:

1. Implement Service login and API calls in the Worker.
2. Add the polling loop, cancellation and recovery from temporary failures.

GitHub issue: [#19](https://github.com/Testik207/Eelarveanaluutik-project/issues/19).

## T20: Show authenticated live import progress

Labels: backend, frontend, reaalajas. Due: 2026-12-02.

Acceptance criteria:

- [ ] /ws/import validates JWT, including browser access_token support.
- [ ] ImportProgress with processed and total, and ImportCompleted, go only to the owning user's channel after persistence.
- [ ] The browser displays progress, refreshes completed data and can recover status after reconnecting.

Commit steps:

1. Implement authenticated per-user WebSocket messages after persistence.
2. Connect the React progress view and reconnect handling.

GitHub issue: [#20](https://github.com/Testik207/Eelarveanaluutik-project/issues/20).

## T21: Create monthly reports from the Worker schedule

Labels: backend, taustateenus, api. Due: 2026-12-02.

Acceptance criteria:

- [ ] POST /api/reports/monthly produces the requested month's report with suitable role checks.
- [ ] The Worker requests the previous month's report on the first day of the new month using a documented timezone.
- [ ] Repeated scheduling or a Worker restart does not create duplicate reports for the same user and month.

Commit steps:

1. Add monthly report creation with duplicate protection.
2. Add the first-day schedule in the Worker.

GitHub issue: [#21](https://github.com/Testik207/Eelarveanaluutik-project/issues/21).

## T22: Add expense distribution and history charts

Labels: frontend. Due: 2026-12-03.

Acceptance criteria:

- [ ] A chart shows expenses by category for the selected period.
- [ ] A chart shows expense changes across months.
- [ ] Labels, values and empty states agree with the API data.

Commit steps:

1. Build the expense-by-category chart.
2. Build the monthly expense history chart and empty states.

GitHub issue: [#22](https://github.com/Testik207/Eelarveanaluutik-project/issues/22).

## T23: Add an explainable next-month expense forecast

Labels: backend, frontend. Due: 2026-12-03.

Acceptance criteria:

- [ ] A simple documented method estimates next month's expenses from historical data.
- [ ] The UI states the method and handles insufficient history.
- [ ] Deterministic examples verify the calculation without implying financial certainty.

Commit steps:

1. Implement and test a simple next-month forecasting method.
2. Show the forecast and explanation in React.

GitHub issue: [#23](https://github.com/Testik207/Eelarveanaluutik-project/issues/23).

## T24: Build the MAUI client with MVVM

Labels: mobiil, frontend, autentimine. Due: 2026-12-03.

Acceptance criteria:

- [ ] MAUI uses MVVM and the same API and user accounts as the site.
- [ ] The client supports login, transaction viewing and creation, and a monthly summary.
- [ ] The client builds and runs on Windows and an Android target is demonstrated; API connectivity instructions are documented.

Commit steps:

1. Create MAUI MVVM structure and API login.
2. Add transaction and summary screens.
3. Check Windows and Android startup instructions against actual runs.

GitHub issue: [#24](https://github.com/Testik207/Eelarveanaluutik-project/issues/24).

## T25: Complete the required unit test coverage

Labels: testid, backend. Due: 2026-12-04.

Acceptance criteria:

- [ ] At least eight meaningful unit tests pass, including two using mocked dependencies.
- [ ] Coverage includes calculations, category rules, validation or import behaviour as appropriate.
- [ ] Tests are added alongside features and the README gives the test command.

Commit steps:

1. Complete missing unit tests for calculations, rules or imports.
2. Add the required mocked-dependency tests and record the test command.

GitHub issue: [#25](https://github.com/Testik207/Eelarveanaluutik-project/issues/25).

## T26: Verify the complete application and fix integration issues

Labels: testid. Due: 2026-12-04.

Acceptance criteria:

- [ ] A clean setup starts the API, website, Worker and second client using documented commands.
- [ ] Checks include two users, malformed CSV, repeated processing, Worker restart and WebSocket reconnect.
- [ ] Discovered defects have concrete fix commits and test results are recorded accurately.

Commit steps:

1. Run the complete setup and integration checklist.
2. Fix each actual defect in its own relevant commit and record the final results.

GitHub issue: [#26](https://github.com/Testik207/Eelarveanaluutik-project/issues/26).

## T27: Prepare the Estonian report and project demonstration

Labels: dokumentatsioon. Due: 2026-12-04.

Acceptance criteria:

- [ ] The English README covers setup, configuration, roles, CSV format and all applications.
- [ ] An Estonian report follows the supplied school guide and is exported to PDF.
- [ ] The GitHub link, PDF and a short demonstration script are ready for submission.

Commit steps:

1. Finish the English setup README and demo instructions.
2. Write the Estonian report and export the school-formatted PDF.

GitHub issue: [#27](https://github.com/Testik207/Eelarveanaluutik-project/issues/27).

## T28: Configure a shared PostgreSQL database

Labels: andmebaas, backend. Due: 2026-11-28.

Acceptance criteria:

- [ ] A managed PostgreSQL database is available independently of either development PC.
- [ ] Only the API uses the Npgsql EF Core provider and a private connection string.
- [ ] The API connection can be configured on each PC and checked on the school network; credentials are not committed.

Commit steps:

1. Add PostgreSQL provider and external connection configuration.
2. Document connection setup and verify connectivity.

GitHub issue: [#28](https://github.com/Testik207/Eelarveanaluutik-project/issues/28).

## T29: Document setup after the school PC is cleared

Labels: dokumentatsioon, testid. Due: 2026-11-30.

Acceptance criteria:

- [ ] The instructions cover cloning the current branch, restoring .NET and npm dependencies and restoring local configuration.
- [ ] No existing cloud records are reset or replaced during normal startup.
- [ ] The workflow explains pushing changes before leaving school and verifies access to existing cloud data after a fresh checkout.

Commit steps:

1. Document the VS Code and Windows setup and two-PC workflow.
2. Check a fresh checkout and correct the instructions using actual results.

GitHub issue: [#29](https://github.com/Testik207/Eelarveanaluutik-project/issues/29).
