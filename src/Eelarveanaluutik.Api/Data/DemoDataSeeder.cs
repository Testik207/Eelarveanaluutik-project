using Eelarveanaluutik.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Eelarveanaluutik.Api.Data;

public static class DemoDataSeeder
{
    public static readonly Guid UserId = Guid.Parse("10000000-0000-0000-0000-000000000001");

    public static void Seed(DbContext context)
    {
        foreach (var (id, entity) in CreateRecords())
        {
            if (context.Find(entity.GetType(), id) is null)
            {
                context.Add(entity);
            }
        }
        context.SaveChanges();
    }

    public static async Task SeedAsync(DbContext context, CancellationToken cancellationToken)
    {
        foreach (var (id, entity) in CreateRecords())
        {
            if (await context.FindAsync(entity.GetType(), [id], cancellationToken) is null)
            {
                context.Add(entity);
            }
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<(Guid Id, object Entity)> CreateRecords()
    {
        var created = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc);
        var salary = Guid.Parse("20000000-0000-0000-0000-000000000001");
        var food = Guid.Parse("20000000-0000-0000-0000-000000000002");
        var transport = Guid.Parse("20000000-0000-0000-0000-000000000003");
        var user = new AppUser
        {
            Id = UserId, Email = "demo@example.invalid", NormalizedEmail = "DEMO@EXAMPLE.INVALID",
            DisplayName = "Demo", CreatedAtUtc = created
        };
        yield return (user.Id, user);
        foreach (var (id, name) in new[] { (salary, "Palk"), (food, "Toit"), (transport, "Transport") })
        {
            yield return (id, new Category { Id = id, UserId = UserId, Name = name, NormalizedName = name.ToUpperInvariant() });
        }
        var transactions = new[]
        {
            new Transaction { Id = Guid.Parse("30000000-0000-0000-0000-000000000001"), CategoryId = salary, Amount = 1500m, Type = TransactionType.Income, Date = new DateOnly(2026, 11, 1), Description = "Näidispalk" },
            new Transaction { Id = Guid.Parse("30000000-0000-0000-0000-000000000002"), CategoryId = food, Amount = 12.50m, Type = TransactionType.Expense, Date = new DateOnly(2026, 11, 2), Description = "Rimi näidisost" },
            new Transaction { Id = Guid.Parse("30000000-0000-0000-0000-000000000003"), CategoryId = transport, Amount = 35m, Type = TransactionType.Expense, Date = new DateOnly(2026, 11, 3), Description = "Näidispilet" }
        };
        foreach (var transaction in transactions)
        {
            transaction.UserId = UserId;
            transaction.CreatedAtUtc = created;
            yield return (transaction.Id, transaction);
        }
        var budget = new MonthlyBudget
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000001"), UserId = UserId,
            Month = new DateOnly(2026, 11, 1), Amount = 600m
        };
        yield return (budget.Id, budget);
        var rule = new CategoryRule
        {
            Id = Guid.Parse("50000000-0000-0000-0000-000000000001"), UserId = UserId,
            CategoryId = food, Keyword = "Rimi", Priority = 1
        };
        yield return (rule.Id, rule);
    }
}
