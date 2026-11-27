using Eelarveanaluutik.Api.Models;

namespace Eelarveanaluutik.Api.Repositories;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AppUser?> FindByNormalizedEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);
    Task AddAsync(AppUser user, CancellationToken cancellationToken = default);
}
