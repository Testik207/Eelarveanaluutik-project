using Eelarveanaluutik.Api.Data;
using Eelarveanaluutik.Api.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Eelarveanaluutik.Tests;

[Trait("Category", "PostgreSQL")]
public class DatabaseTests(PostgreSqlFixture fixture) : IClassFixture<PostgreSqlFixture>
{
    [PostgreSqlFact]
    public async Task MigrationCreatesEightApplicationTablesAndCanRunAgain()
    {
        await using var context = fixture.CreateContext();
        await context.Database.MigrateAsync();
        Assert.Single(await context.Database.GetAppliedMigrationsAsync());
        Assert.Empty(await context.Database.GetPendingMigrationsAsync());
        await using var connection = new NpgsqlConnection(fixture.ConnectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT count(*) FROM information_schema.tables WHERE table_schema = current_schema() AND table_name <> '__EFMigrationsHistory'", connection);
        Assert.Equal(8L, await command.ExecuteScalarAsync());
    }

    [PostgreSqlFact]
    public async Task SeedHooksDoNotDuplicateRecordsOrOverwriteEdits()
    {
        await using (var first = fixture.CreateContext(seed: true))
        {
            await first.Database.MigrateAsync();
            var category = await first.Categories.SingleAsync(x => x.UserId == DemoDataSeeder.UserId && x.Name == "Toit");
            category.Name = "Muudetud";
            category.NormalizedName = "MUUDETUD";
            await first.SaveChangesAsync();
        }
        await using var second = fixture.CreateContext(seed: true);
        second.Database.Migrate();
        Assert.Equal(3, await second.Categories.CountAsync(x => x.UserId == DemoDataSeeder.UserId));
        Assert.Equal(3, await second.Transactions.CountAsync(x => x.UserId == DemoDataSeeder.UserId));
        Assert.True(await second.Categories.AnyAsync(x => x.UserId == DemoDataSeeder.UserId && x.Name == "Muudetud"));
        Assert.Equal(1500m, await second.Transactions.Where(x => x.UserId == DemoDataSeeder.UserId && x.Type == TransactionType.Income).SumAsync(x => x.Amount));
        Assert.Equal(47.50m, await second.Transactions.Where(x => x.UserId == DemoDataSeeder.UserId && x.Type == TransactionType.Expense).SumAsync(x => x.Amount));
    }

    [PostgreSqlFact]
    public async Task TransactionCannotReferenceAnotherUsersCategory()
    {
        await using var context = fixture.CreateContext();
        var owner = User();
        var other = User();
        var category = new Category { User = owner, UserId = owner.Id, Name = "Food", NormalizedName = "FOOD" };
        context.AddRange(owner, other, category);
        await context.SaveChangesAsync();
        context.Transactions.Add(Operation(other.Id, category.Id));
        await AssertConstraintAsync(context, PostgresErrorCodes.ForeignKeyViolation);
    }

    [PostgreSqlFact]
    public async Task ImportedRowCannotCreateTwoTransactions()
    {
        await using var context = fixture.CreateContext();
        var user = User();
        var category = new Category { UserId = user.Id, Name = "Food", NormalizedName = "FOOD" };
        var import = new CsvImport { UserId = user.Id, OriginalFileName = "sample.csv", TotalRows = 1 };
        var row = new CsvImportRow { UserId = user.Id, CsvImportId = import.Id, RowNumber = 1, RawData = "sample" };
        context.AddRange(user, category, import, row);
        await context.SaveChangesAsync();
        var first = Operation(user.Id, category.Id);
        first.CsvImportRowId = row.Id;
        context.Add(first);
        await context.SaveChangesAsync();
        var duplicate = Operation(user.Id, category.Id);
        duplicate.CsvImportRowId = row.Id;
        context.Add(duplicate);
        await AssertConstraintAsync(context, PostgresErrorCodes.UniqueViolation);
    }

    [PostgreSqlFact]
    public async Task BudgetRejectsDatesOtherThanFirstDayOfMonth()
    {
        await using var context = fixture.CreateContext();
        var user = User();
        context.Add(user);
        await context.SaveChangesAsync();
        context.Add(new MonthlyBudget { UserId = user.Id, Month = new DateOnly(2026, 11, 15), Amount = 100m });
        await AssertConstraintAsync(context, PostgresErrorCodes.CheckViolation);
    }

    [PostgreSqlFact]
    public async Task AmountAndDateSurvivePostgreSqlRoundTrip()
    {
        await using var context = fixture.CreateContext();
        var user = User();
        var category = new Category { UserId = user.Id, Name = "Food", NormalizedName = "FOOD" };
        var transaction = Operation(user.Id, category.Id);
        context.AddRange(user, category, transaction);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        var saved = await context.Transactions.SingleAsync(x => x.Id == transaction.Id);
        Assert.Equal(12.34m, saved.Amount);
        Assert.Equal(new DateOnly(2026, 11, 27), saved.Date);
        Assert.Equal(DateTimeKind.Utc, saved.CreatedAtUtc.Kind);
    }

    private static AppUser User()
    {
        var email = Guid.NewGuid().ToString("N") + "@example.invalid";
        return new AppUser { Email = email, NormalizedEmail = email.ToUpperInvariant(), DisplayName = "Test" };
    }

    private static Transaction Operation(Guid userId, Guid categoryId) => new()
    {
        UserId = userId, CategoryId = categoryId, Amount = 12.34m, Type = TransactionType.Expense,
        Date = new DateOnly(2026, 11, 27), Description = "Test"
    };

    private static async Task AssertConstraintAsync(BudgetDbContext context, string sqlState)
    {
        var error = await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesAsync());
        Assert.Equal(sqlState, Assert.IsType<PostgresException>(error.InnerException).SqlState);
    }
}
