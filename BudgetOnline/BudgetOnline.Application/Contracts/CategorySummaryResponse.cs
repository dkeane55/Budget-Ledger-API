namespace BudgetOnline.Application.Contracts;

public record CategorySummaryResponse(
    Guid CategoryId,
    string CategoryName,
    decimal Budget,
    decimal TotalSpent,
    decimal RemainingBudget
);
