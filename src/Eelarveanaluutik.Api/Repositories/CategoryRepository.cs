using Eelarveanaluutik.Api.Data;
using Eelarveanaluutik.Api.Models;
using Eelarveanaluutik.Api.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Eelarveanaluutik.Api.Repositories;

public class CategoryRepository(BudgetDbContext context) : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> ListForUserAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await context.Categories.AsNoTracking().Where(x => x.UserId == userId)
            .OrderBy(x => x.Name).ThenBy(x => x.Id).ToListAsync(cancellationToken);

    public Task<Category?> GetForUserAsync(Guid userId, Guid id, CancellationToken cancellationToken = default) =>
        context.Categories.SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id, cancellationToken);

    public Task<bool> NameExistsAsync(Guid userId, string normalizedName, CancellationToken cancellationToken = default) =>
        context.Categories.AnyAsync(x => x.UserId == userId && x.NormalizedName == normalizedName, cancellationToken);

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        context.Categories.Add(category);
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException error) when (error.InnerException is PostgresException
               { SqlState: PostgresErrorCodes.UniqueViolation, ConstraintName: "IX_Categories_UserId_NormalizedName" })
        {
            context.Entry(category).State = EntityState.Detached;
            throw new CategoryNameConflictException();
        }
    }
}
