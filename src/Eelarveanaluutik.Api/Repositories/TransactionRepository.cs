using Eelarveanaluutik.Api.Data;
using Eelarveanaluutik.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Eelarveanaluutik.Api.Repositories;

public class TransactionRepository(BudgetDbContext context) : ITransactionRepository
{
    public Task<Transaction?> GetForUserAsync(Guid userId, Guid id, CancellationToken cancellationToken = default) =>
        context.Transactions.SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id, cancellationToken);

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync(cancellationToken);
    }
}
