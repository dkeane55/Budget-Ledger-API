namespace BudgetOnline.Application.Contracts;

public record ErrorResponse(
    int StatusCode,
    string Message,
    string? Details = null
);
