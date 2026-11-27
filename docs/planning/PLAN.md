# Development plan

Target: the requirements for grade 5 in both subjects. The supplied assignment estimates 64 hours. This is a work breakdown, not a claim that development has already happened.

## Parts

| Part | Tasks | Result |
| --- | --- | --- |
| 1. Planning and setup | T01-T03 | GitHub repository, Project, issues and a buildable scaffold |
| 2. Database | T04-T06, T28 | Related tables, migrations, seed data, repositories and services |
| 3. API foundation | T07-T09 | JWT, roles, DTOs, validation, errors, logging and Swagger |
| 4. Transactions | T10-T12 | CRUD, search, filters, sorting, pagination and monthly totals |
| 5. Website | T13-T15, T29 | Login, transaction screens, monthly summary and budgets |
| 6. CSV imports | T16-T18 | Category rules, upload/status endpoints and batch processing |
| 7. Distributed work | T19-T21 | Worker, live import progress and monthly reports |
| 8. Analytics and mobile | T22-T24 | Charts, forecast and MAUI client |
| 9. Submission | T25-T27 | Required tests, end-to-end checks and documentation |

T-numbers identify planned tasks. They are not GitHub issue numbers until the issues are created and mapped.

## Order and dependencies

1. Create the remote repository, board and issues before implementing application features.
2. Build the database and API foundations before the clients use them.
3. Complete manual transactions before CSV imports.
4. Implement batch endpoints before connecting the Worker.
5. Persist a processed batch before notifying the browser.
6. Implement monthly report creation before enabling its schedule.
7. Connect MAUI to the same API and account data as the website.
8. Add tests alongside the relevant features, then run the complete suite before submission.

## Design choices

- Keep a small solution with clear responsibilities. Avoid extra frameworks unless a requirement needs them.
- Use C# for the API, Worker and mobile logic. Use React with JavaScript for the website.
- Use managed PostgreSQL through the Npgsql EF Core provider. The shared database must remain available when either local development folder is deleted. Provisioning and a school-network connection check are still pending.
- Keep EF Core and database access inside the API-side data layer.
- Use decimal amounts and an explicit transaction type for income/expense.
- Define one documented CSV format and provide synthetic sample data.
- Choose a simple forecast that can be explained during the defence. Confirm whether the teacher expects a specific method.
- The proposed MAUI scope is login, a transaction list, adding a transaction and a monthly summary. Confirm that this is sufficient for the second-client requirement.

## Finishing a task

Check the acceptance criteria, build the affected projects, run the relevant checks and commit the result on the issue branch. Add the branch or PR URL to the issue. Close the issue only when its criteria are met.

Keep an English README with exact commands and an Estonian project report. Update documentation as features become usable.

## Dates

Submission is due on 4 December 2026. Planned dates: setup on 27 November; database and API foundation on 28 November; transactions on 29 November; website on 30 November; imports on 1 December; Worker and WebSocket on 2 December; charts, forecast and mobile on 3 December; final verification and submission documents on 4 December. Tests and documentation are developed alongside each feature.

## Before starting application code

- Remote repository: https://github.com/Testik207/Eelarveanaluutik-project.
- Create the GitHub Project and labelled issues with explicit due dates.
- Obtain the school documentation guide before final PDF formatting.

The developer machine is Windows. A Windows or Android MAUI run must be demonstrated; a Linux-only build does not prove the mobile application works.
