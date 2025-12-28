namespace BudgetOnline.Application.Contracts;

public record TransactionResponse(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    string CategoryName
);
