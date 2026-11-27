# Database model

All amounts are in EUR. Amounts use C# `decimal` and PostgreSQL `numeric(18,2)`.
Transaction amounts are positive; `Income` and `Expense` describe the direction.
Transaction dates use `DateOnly` and PostgreSQL `date` (`yyyy-MM-dd` in the API).
Budget and report months are stored as the first day of the month.
Creation and completion timestamps are UTC.

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

PostgreSQL connection and migration commands are added with the persistence setup.
The Worker and React client never access the database directly.
