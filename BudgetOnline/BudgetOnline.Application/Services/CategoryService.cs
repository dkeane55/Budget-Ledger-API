using BudgetOnline.Application.Abstractions;
using BudgetOnline.Application.Contracts;
using BudgetOnline.Application.Interfaces;
using BudgetOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetOnline.Application.Services;

public class CategoryService(IApplicationDbContext context) : ICategoryService
{
    private readonly IApplicationDbContext _context = context;

    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryResponse(c.Id, c.Name))
            .ToListAsync();
    }

    public async Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct)
    {
        var exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == request.Name.ToLower());
        if (exists) return null;

        var category = new Category
        (
            Guid.NewGuid(),
            request.Name,
            request.Budget
        );
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(ct);

        var response = new CategoryResponse(category.Id, category.Name);

        return new CategoryResponse(category.Id, category.Name);
    }

    public async Task<IEnumerable<CategorySummaryResponse>> GetCategoryBudgetSummaryAsync()
    {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year, now.Month, 1);
        var end = start.AddMonths(1).AddTicks(-1);

        var summaryData = await _context.Categories
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Budget,
                TotalSpent = (decimal)(_context.Transactions
                    .Where(t => t.CategoryId == c.Id && t.Date >= start && t.Date < end)
                    .Sum(t => (double?)t.Amount) ?? 0)
            })
            .ToListAsync();

        return summaryData.Select(s => new CategorySummaryResponse(
            s.Id,
            s.Name,
            s.Budget,
            s.TotalSpent,
            s.Budget - s.TotalSpent
        ));
    }
}