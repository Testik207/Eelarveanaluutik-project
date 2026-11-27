using Eelarveanaluutik.Api.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Eelarveanaluutik.Tests;

public sealed class PostgreSqlFactAttribute : FactAttribute
{
    public PostgreSqlFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEST_POSTGRES_CONNECTION")))
        {
            Skip = "Set TEST_POSTGRES_CONNECTION to run PostgreSQL integration tests.";
        }
    }
}

public sealed class PostgreSqlFixture : IAsyncLifetime
{
    private readonly string schema = "test_" + Guid.NewGuid().ToString("N");
    private string? baseConnection;
    public string ConnectionString { get; private set; } = string.Empty;

    public BudgetDbContext CreateContext(bool seed = false)
    {
        var options = new DbContextOptionsBuilder<BudgetDbContext>().UseNpgsql(ConnectionString);
        if (seed)
        {
            options.UseSeeding((context, _) => DemoDataSeeder.Seed(context));
            options.UseAsyncSeeding((context, _, token) => DemoDataSeeder.SeedAsync(context, token));
        }
        return new BudgetDbContext(options.Options);
    }

    public async Task InitializeAsync()
    {
        baseConnection = Environment.GetEnvironmentVariable("TEST_POSTGRES_CONNECTION");
        if (string.IsNullOrWhiteSpace(baseConnection)) return;

        await using var connection = new NpgsqlConnection(baseConnection);
        await connection.OpenAsync();
        // The schema name consists only of a fixed prefix and a generated GUID.
        await using var create = new NpgsqlCommand($"CREATE SCHEMA \"{schema}\"", connection);
        await create.ExecuteNonQueryAsync();
        ConnectionString = new NpgsqlConnectionStringBuilder(baseConnection) { SearchPath = schema }.ConnectionString;
        await using var context = CreateContext();
        await context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        if (string.IsNullOrWhiteSpace(baseConnection)) return;
        await using var connection = new NpgsqlConnection(baseConnection);
        await connection.OpenAsync();
        await using var drop = new NpgsqlCommand($"DROP SCHEMA IF EXISTS \"{schema}\" CASCADE", connection);
        await drop.ExecuteNonQueryAsync();
    }
}
