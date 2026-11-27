using Eelarveanaluutik.Api.Data;
using Eelarveanaluutik.Api.Models;
using Eelarveanaluutik.Api.Repositories;
using Eelarveanaluutik.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace Eelarveanaluutik.Tests;

[Trait("Category", "PostgreSQL")]
public class RepositoryTests(PostgreSqlFixture fixture) : IClassFixture<PostgreSqlFixture>
{
    [PostgreSqlFact]
    public async Task RepositoriesDoNotReturnAnotherUsersCategoriesOrTransactions()
    {
        await using var context = fixture.CreateContext();
        var users = new UserRepository(context);
        var categories = new CategoryRepository(context);
        var transactions = new TransactionRepository(context);
        var owner = User();
        var other = User();
        await users.AddAsync(owner);
        await users.AddAsync(other);
        var category = new Category { UserId = owner.Id, Name = "Food", NormalizedName = "FOOD" };
        await categories.AddAsync(category);
        var transaction = new Transaction
        {
            UserId = owner.Id, CategoryId = category.Id, Amount = 10m,
            Type = TransactionType.Expense, Date = new DateOnly(2026, 11, 1)
        };
        await transactions.AddAsync(transaction);
        Assert.Single(await categories.ListForUserAsync(owner.Id));
        Assert.Empty(await categories.ListForUserAsync(other.Id));
        Assert.Null(await categories.GetForUserAsync(other.Id, category.Id));
        Assert.Null(await transactions.GetForUserAsync(other.Id, transaction.Id));
        Assert.NotNull(await transactions.GetForUserAsync(owner.Id, transaction.Id));
    }

    [PostgreSqlFact]
    public async Task DuplicateCategoryInsertIsTranslatedToAnApplicationConflict()
    {
        await using var context = fixture.CreateContext();
        var user = User();
        await new UserRepository(context).AddAsync(user);
        var repository = new CategoryRepository(context);
        await repository.AddAsync(new Category { UserId = user.Id, Name = "Food", NormalizedName = "FOOD" });
        await Assert.ThrowsAsync<CategoryNameConflictException>(() => repository.AddAsync(
            new Category { UserId = user.Id, Name = "food", NormalizedName = "FOOD" }));
        Assert.Single(await repository.ListForUserAsync(user.Id));
    }

    [PostgreSqlFact]
    public async Task RegisteredServicesSaveAndReadCategoriesAcrossScopes()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["ConnectionStrings:BudgetDatabase"] = fixture.ConnectionString
        }).Build();
        var environment = Mock.Of<IHostEnvironment>(x => x.EnvironmentName == Environments.Development);
        var services = new ServiceCollection();
        services.AddPersistence(configuration, environment);
        services.AddApplicationServices();
        await using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true });
        var user = User();
        await using (var scope = provider.CreateAsyncScope())
        {
            var users = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            await users.AddAsync(user);
            Assert.Equal(user.Id, (await users.FindByNormalizedEmailAsync(user.NormalizedEmail))?.Id);
            var categories = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            await categories.CreateAsync(user.Id, "  Transport  ");
        }
        await using var nextScope = provider.CreateAsyncScope();
        var category = Assert.Single(await nextScope.ServiceProvider.GetRequiredService<ICategoryService>().ListAsync(user.Id));
        Assert.Equal("Transport", category.Name);
    }

    private static AppUser User()
    {
        var email = Guid.NewGuid().ToString("N") + "@example.invalid";
        return new AppUser { Email = email, NormalizedEmail = email.ToUpperInvariant(), DisplayName = "Test" };
    }
}
