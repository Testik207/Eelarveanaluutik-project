# Database model

All amounts are in EUR. Amounts use C# `decimal` and PostgreSQL `numeric(18,2)`.
Transaction amounts are positive; `Income` and `Expense` describe the direction.
Transaction dates use `DateOnly` and PostgreSQL `date` (`yyyy-MM-dd` in the API).
Budget and report months are stored as the first day of the month.
Creation and completion timestamps are UTC.
Import `ProcessedRows` includes successful and failed rows; `FailedRows` is a
subset of processed rows.

| Table | Purpose | Relationships |
| --- | --- | --- |
| Users | Email, password hash and User/Admin/Service role | Owns application records |
| Categories | Named categories per user | User |
| Transactions | Dated income and expenses | User, category, optional import row |
| MonthlyBudgets | One budget per user and month | User |
| CategoryRules | Description keywords and rule priority | User, category |
| CsvImports | Uploaded file status and progress counters | User |
| CsvImportRows | Stored input rows and processing results | Import and its owner |
| MonthlyReports | Saved monthly income and expense totals | User |

Category references include the owner ID so an operation or rule cannot use
another user's category. Imported transactions also reference a row belonging
to the same owner. An import row can produce at most one transaction.
Each row number is unique within an import. These constraints support the
later batch-processing and retry implementation; processing itself is not implemented yet.

Each user has unique category names after trimming and converting to uppercase.
Normalized email addresses are unique. Budgets and reports are unique by user and month.
Referenced categories cannot be deleted while transactions or rules use them.
Deleting an import is restricted while its rows have transactions.

## Setup

Use an existing PostgreSQL database and its private connection details. The shared
cloud database still needs to be created and checked on both development PCs (T28).
Do not put its password in this file or in `appsettings.json`.

From the repository root in PowerShell:

```powershell
dotnet tool restore
dotnet user-secrets set "ConnectionStrings:BudgetDatabase" "Host=YOUR_HOST;Port=5432;Database=YOUR_DATABASE;Username=YOUR_USER;Password=YOUR_PASSWORD;SSL Mode=VerifyFull" --project src/Eelarveanaluutik.Api
dotnet run --project src/Eelarveanaluutik.Api --launch-profile http -- --check-database
dotnet ef database update --project src/Eelarveanaluutik.Api -- --environment Development
```

Replace the placeholders with the database provider's details. Keep the provider's
required TLS settings. `ConnectionStrings__BudgetDatabase` is the equivalent
environment variable. User secrets are stored locally and must be configured on
each PC. The Worker and React client never receive this connection string.

Migrations are applied explicitly, not on every API startup. Running the update
command again keeps existing data. `/api/health` checks the server only and can
still run without a configured database. `--check-database` checks connectivity
and exits; it does not create tables or prove that migrations are current.

## Optional demo data

The migration creates empty tables by default. To add synthetic examples in Development:

```powershell
dotnet user-secrets set "SeedDemoData" "true" --project src/Eelarveanaluutik.Api
dotnet ef database update --project src/Eelarveanaluutik.Api -- --environment Development
dotnet user-secrets remove "SeedDemoData" --project src/Eelarveanaluutik.Api
```

This adds `demo@example.invalid`, three categories, three transactions, a budget
and a Rimi category rule for November 2026. The demo user has no password hash
and cannot be used to log in. Authentication is added in T07. Expected totals:
income EUR 1500.00, expenses EUR 47.50, balance EUR 1452.50.

Fixed seed IDs prevent duplicate records. Existing seed records are left as they
are, including edits. Both synchronous and asynchronous seed hooks are configured
under the EF migration lock. Deleted demo records are restored when seeding is
explicitly enabled again. Demo seeding is rejected outside Development.

## Checks

```powershell
dotnet ef migrations has-pending-model-changes --project src/Eelarveanaluutik.Api -- --environment Development
dotnet ef migrations script --idempotent --project src/Eelarveanaluutik.Api --output schema.sql -- --environment Development
```

The first command checks whether the model differs from the committed migration.
The second generates SQL for review; it does not execute it or run demo seeding.

## Application access

`BudgetDbContext` and all EF Core repositories belong to the API project.
`UserRepository`, `CategoryRepository` and `TransactionRepository` are registered
as scoped services. Category and transaction lookups require an owner ID.
The category service validates and normalizes names, checks for duplicates and
returns `CategoryDto` values. A unique database index also catches duplicate
names if simultaneous requests pass the initial service check.

Category endpoints and authentication are not exposed yet. They are added in
T07-T10. The remaining repositories and operations are added as their features
need them. The Worker and contracts projects have no EF Core dependency.

## Verified results

Checked on 27 November 2026 with .NET SDK 10.0.404 and PostgreSQL 17.12 on Linux:

- Solution build: no warnings or errors.
- Eight unit test cases and nine PostgreSQL integration tests passed.
- Migration applied to an empty schema and applied again without changes.
- Both seed hooks ran without duplicates and kept an edited category name.
- Foreign keys rejected another user's category, and a unique index rejected
  a second transaction for the same import row.
- EF reported no pending model changes.

The shared cloud database and school-network connection are still unverified (T28).
