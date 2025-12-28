using BudgetOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetOnline.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Transaction> Transactions { get; }
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}