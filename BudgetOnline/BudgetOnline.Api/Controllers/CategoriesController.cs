using BudgetOnline.Application.Contracts;
using BudgetOnline.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetOnline.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
    {
        var result = await _categoryService.GetAllCategoriesAsync();
        return Ok(result);
    }

    [HttpPost]  
    public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryRequest request, CancellationToken ct)
    {
        var response = await _categoryService.CreateCategoryAsync(request, ct);

        if (response == null)
            return BadRequest("A category with this name already exists");

        return CreatedAtAction(nameof(GetCategories), new { id = response.Id }, response);
    }

    [HttpGet("summary")]
    public async Task<ActionResult<IEnumerable<CategorySummaryResponse>>> GetCategoryBudgetSummary()
    {
        var response = await _categoryService.GetCategoryBudgetSummaryAsync();
        return Ok(response);
    }   
}
