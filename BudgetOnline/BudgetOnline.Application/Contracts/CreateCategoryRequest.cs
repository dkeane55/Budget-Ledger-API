using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Application.Contracts;

public record CreateCategoryRequest(
   
   [Required]
   [StringLength(50)]
   string Name,

   [Range(0, double.MaxValue)]
   decimal Budget
);

