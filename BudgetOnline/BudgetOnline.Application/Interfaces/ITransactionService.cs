using BudgetOnline.Application.Contracts;

namespace BudgetOnline.Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponse>> GetAllTransactionsAsync();
    Task<TransactionResponse?> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken ct);
    Task<bool> UpdateTransactionAsync(Guid id, UpdateTransactionRequest request, CancellationToken ct);
    Task<bool> DeleteTransactionAsync(Guid id, CancellationToken ct);
}
