using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Api.Contracts;

public record CreateTransactionRequest(
    [Required]
    string Description,
    [Range(0.01, double.MaxValue)] 
    decimal Amount,
    DateTime Date
);
