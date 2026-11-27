using Eelarveanaluutik.Api.Models;

namespace Eelarveanaluutik.Api.Repositories;

public interface ITransactionRepository
{
    Task<Transaction?> GetForUserAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
}
