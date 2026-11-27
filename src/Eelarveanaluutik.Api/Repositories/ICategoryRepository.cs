using Eelarveanaluutik.Api.Models;

namespace Eelarveanaluutik.Api.Repositories;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> ListForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Category?> GetForUserAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(Guid userId, string normalizedName, CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
}
