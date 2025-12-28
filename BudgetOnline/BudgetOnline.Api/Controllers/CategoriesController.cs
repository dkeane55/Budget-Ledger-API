using BudgetOnline.Api.Contracts;
using BudgetOnline.Domain.Entities;
using BudgetOnline.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BudgetOnline.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
    {
        var categories = await context.Categories
            .Select(c => new CategoryResponse(c.Id, c.Name))
            .ToListAsync();

        return Ok(categories);
    }

    [HttpPost]  
    public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryRequest request)
    {
        var exists = await context.Categories.AnyAsync(c => c.Name.ToLower() == request.Name.ToLower());
        if (exists) return BadRequest("A category with this name already exists");
        
        var category = new Category
        (
            Guid.NewGuid(),
            request.Name,
            request.Budget
        );
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var response = new CategoryResponse(category.Id, category.Name);

        return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, response);
    }

    [HttpGet("summary")]
    public async Task<ActionResult<IEnumerable<CategorySummaryResponse>>> GetCategoryBudgetSummary()
    {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year, now.Month, 1);
        var end = start.AddMonths(1).AddTicks(-1);

        var summary = await context.Categories
        .Select(c => new
        {
            c.Id,
            c.Name,
            c.Budget,
            TotalSpent = (decimal)(context.Transactions
                .Where(t => t.CategoryId == c.Id && t.Date >= start && t.Date < end)
                .Sum(t => (double?)t.Amount) ?? 0)
        })
        .ToListAsync();

        var response = summary.Select(s => new CategorySummaryResponse(
            s.Id,
            s.Name,
            s.Budget,
            s.TotalSpent,
            s.Budget - s.TotalSpent
        ));

        return Ok(response);
    }   
}
