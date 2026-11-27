using Eelarveanaluutik.Api.Models;
using Eelarveanaluutik.Api.Repositories;
using Eelarveanaluutik.Contracts;

namespace Eelarveanaluutik.Api.Services;

public class CategoryService(ICategoryRepository categories) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryDto>> ListAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var records = await categories.ListForUserAsync(userId, cancellationToken);
        return records.Select(x => new CategoryDto(x.Id, x.Name)).ToList();
    }

    public async Task<CategoryDto> CreateAsync(Guid userId, string name, CancellationToken cancellationToken = default)
    {
        var trimmed = name?.Trim();
        if (string.IsNullOrWhiteSpace(trimmed) || trimmed.Length > 80)
        {
            throw new ArgumentException("Category name must contain between 1 and 80 characters.", nameof(name));
        }
        var normalized = trimmed.ToUpperInvariant();
        if (await categories.NameExistsAsync(userId, normalized, cancellationToken))
        {
            throw new CategoryNameConflictException();
        }
        var category = new Category { UserId = userId, Name = trimmed, NormalizedName = normalized };
        await categories.AddAsync(category, cancellationToken);
        return new CategoryDto(category.Id, category.Name);
    }
}
