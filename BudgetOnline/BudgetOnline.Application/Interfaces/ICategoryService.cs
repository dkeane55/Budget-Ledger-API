using BudgetOnline.Application.Contracts;

namespace BudgetOnline.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
    Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct);
    Task<IEnumerable<CategorySummaryResponse>> GetCategoryBudgetSummaryAsync();
}