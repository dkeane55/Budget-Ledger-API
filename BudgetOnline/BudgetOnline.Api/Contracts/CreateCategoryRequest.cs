using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Api.Contracts;

public record CreateCategoryRequest(
   
   [Required]
   [StringLength(50)]
   string Name,

   [Range(0, double.MaxValue)]
   decimal Budget
);

