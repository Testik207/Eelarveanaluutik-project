using Microsoft.EntityFrameworkCore;

namespace Eelarveanaluutik.Api.Data;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var seedDemoData = configuration.GetValue<bool>("SeedDemoData");
        if (seedDemoData && !environment.IsDevelopment())
        {
            throw new InvalidOperationException("Demo data can only be enabled in Development.");
        }
        services.AddDbContext<BudgetDbContext>(options =>
        {
            var connection = configuration.GetConnectionString("BudgetDatabase");
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new InvalidOperationException(
                    "Configure ConnectionStrings:BudgetDatabase using user secrets or an environment variable. See docs/DATABASE.md.");
            }

            options.UseNpgsql(connection);
            if (seedDemoData)
            {
                options.UseSeeding((context, _) => DemoDataSeeder.Seed(context));
                options.UseAsyncSeeding((context, _, token) => DemoDataSeeder.SeedAsync(context, token));
            }
        });
        return services;
    }
}
