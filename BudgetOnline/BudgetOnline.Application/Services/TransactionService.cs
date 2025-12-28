using BudgetOnline.Application.Abstractions;
using BudgetOnline.Application.Contracts;
using BudgetOnline.Application.Interfaces;
using BudgetOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetOnline.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IApplicationDbContext _context;
    public TransactionService(IApplicationDbContext context)
    {
        _context = context;
    }   

    public async Task<IEnumerable<TransactionResponse>> GetAllTransactionsAsync()
    {
        var transactions = await _context.Transactions.Include(t => t.Category).ToListAsync();

        return transactions.Select(t => new TransactionResponse(
            t.Id,
            t.Amount,
            t.Description,
            t.Date,
            t.Category.Name
        ));
    }

    public async Task<TransactionResponse?> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken ct)
    {
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId);

        if (!categoryExists) return null;

        var transaction = new Transaction
        (
            Guid.NewGuid(),
            request.Amount,
            request.Description,
            request.Date,
            request.CategoryId
        );
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(ct);

        var category = await _context.Categories.FindAsync(request.CategoryId);

        return new TransactionResponse(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Date,
            category?.Name ?? "Unknown Category"
        );
    }

    public async Task<bool> UpdateTransactionAsync(Guid id, UpdateTransactionRequest request, CancellationToken ct)
    {
        var transaction = await _context.Transactions.FindAsync(id, ct);
        if (transaction == null)
        {
            return false;
        }
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == transaction.CategoryId, ct);
        if (!categoryExists)
        {
            throw new InvalidOperationException("Category does not exist");
        }

        transaction.UpdateDetails(
            request.Description,
            request.Amount,
            request.Date,
            request.CategoryId
        );

        await _context.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteTransactionAsync(Guid id, CancellationToken ct)
    {
        var transaction = await _context.Transactions.FindAsync(id, ct);
        if (transaction == null) return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(ct);

        return true;
    }
}
