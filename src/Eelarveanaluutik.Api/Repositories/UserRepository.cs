using Eelarveanaluutik.Api.Data;
using Eelarveanaluutik.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Eelarveanaluutik.Api.Repositories;

public class UserRepository(BudgetDbContext context) : IUserRepository
{
    public Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<AppUser?> FindByNormalizedEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default) =>
        context.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == normalizedEmail, cancellationToken);

    public async Task AddAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}
