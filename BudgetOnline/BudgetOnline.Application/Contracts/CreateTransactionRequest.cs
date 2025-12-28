using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Application.Contracts;

public record CreateTransactionRequest(
    [Required]
    string Description,
    [Range(0.01, double.MaxValue)] 
    decimal Amount,
    DateTime Date,
    [Required]
    Guid CategoryId
);
