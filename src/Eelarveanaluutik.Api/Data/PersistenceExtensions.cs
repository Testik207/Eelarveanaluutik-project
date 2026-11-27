using Microsoft.EntityFrameworkCore;

namespace Eelarveanaluutik.Api.Data;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BudgetDbContext>(options =>
        {
            var connection = configuration.GetConnectionString("BudgetDatabase");
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new InvalidOperationException(
                    "Configure ConnectionStrings:BudgetDatabase using user secrets or an environment variable. See docs/DATABASE.md.");
            }

            options.UseNpgsql(connection);
        });
        return services;
    }
}
