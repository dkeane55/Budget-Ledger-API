namespace BudgetOnline.Api.Contracts;

public record TransactionResponse(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date
);
