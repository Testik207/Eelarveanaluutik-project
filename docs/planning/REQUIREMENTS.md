# Assignment checklist

Source: the topic and general-requirement screenshots provided in this conversation. Checkboxes show verified completion, not planned work.

Scope clarification from the student on 27 November 2026: a React website does not require a mobile application or a second client for grade 5. The active checklist below follows that clarification.

## Planning and submission

- [ ] GitHub repository and GitHub Project exist before application development.
- [ ] At least 20 concrete issues exist.
- [ ] Every issue has suitable labels and an explicit due date.
- [ ] Completed issues link to a branch or pull request and are closed.
- [ ] Commits are made on every day spent developing.
- [ ] README is in English.
- [ ] Documentation is in Estonian and follows the school guide.
- [ ] A PDF and GitHub link are ready for Moodle.
- [ ] The student can explain and demonstrate the application.

## Programming: common requirements

- [ ] At least five related database tables.
- [ ] EF Core migrations and seed data.
- [ ] CRUD, search, filters, sorting and pagination.
- [ ] Client-side and API validation.
- [ ] Business logic in services, Repository pattern and dependency injection.
- [ ] At least eight unit tests, including two using mocks.
- [ ] A permitted web client: MVC, Blazor or React.

## Budget topic: programming

- [ ] Grade 3: add income and expenses with categories.
- [ ] Grade 3: filters.
- [ ] Grade 3: monthly income, expenses and balance.
- [ ] Grade 4: CSV import.
- [ ] Grade 4: automatic categorisation by rules.
- [ ] Grade 4: monthly budget and overspending warning.
- [ ] Grade 5: expense distribution and change charts.
- [ ] Grade 5: next-month expense forecast.

## Distributed applications: common requirements

- [ ] REST endpoints use suitable HTTP methods and status codes.
- [ ] API returns DTOs rather than EF entities.
- [ ] Swagger includes descriptions and request examples.
- [ ] Clients use the API; CORS is configured for the web client.
- [ ] A Postman collection or `.http` file covers the API.
- [ ] JWT authentication, roles and Swagger Authorize.
- [ ] Consistent ProblemDetails errors and logging.
- [ ] Worker is a separate process and uses its own Service account through the API.

## Budget topic: distributed requirements

- [ ] Roles: User, Admin, Service.
- [ ] `GET /api/transactions?from=&to=&category=`.
- [ ] `POST /api/imports` accepts a CSV and returns 202 with an import ID.
- [ ] `GET /api/imports/{id}` returns import status.
- [ ] CRUD `/api/category-rules`.
- [ ] `GET /api/budgets/{month}/summary`.
- [ ] `GET /api/imports/pending` provides pending work to Service.
- [ ] `POST /api/imports/{id}/process-batch?size=100` processes up to 100 rows.
- [ ] Worker polls every 10 seconds and repeats batches until complete.
- [ ] `POST /api/reports/monthly` creates a monthly report.
- [ ] Worker initiates the report on the first day of the month.
- [ ] Worker uses BackgroundService, PeriodicTimer and HttpClient, with no DbContext.
- [ ] API owns business logic and database access.
- [ ] `/ws/import` authenticates JWT and supports the browser's `access_token` query parameter.
- [ ] Import messages are scoped to `user-{userId}`.
- [ ] `ImportProgress` includes processed/total counts; `ImportCompleted` signals completion.
- [ ] Notifications are sent after data is saved, using a service with DI.
- [ ] Browser receives updates over WebSocket.

## Details to confirm

- Exact submission and intermediate-review dates.
- Required SDK version if specified separately by the teacher.
- School report formatting guide.
- Whether a specific forecasting method is expected.
