using Eelarveanaluutik.Contracts;

namespace Eelarveanaluutik.Api.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> ListAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<CategoryDto> CreateAsync(Guid userId, string name, CancellationToken cancellationToken = default);
}
