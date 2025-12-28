namespace BudgetOnline.Application.Contracts;

public record UpdateTransactionRequest(
    decimal Amount,
    string Description,
    DateTime Date,
    Guid CategoryId
);
