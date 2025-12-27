using BudgetOnline.Api.Contracts;
using BudgetOnline.Domain.Entities;
using BudgetOnline.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetOnline.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        var transactions = await context.Transactions.ToListAsync();

        var response = transactions.Select(t => new TransactionResponse(
            t.Id,
            t.Amount,
            t.Description,
            t.Date
        ));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionRequest request)
    {
        var categoryExists = await context.Categories.AnyAsync(c => c.Id == request.CategoryId);

        if (!categoryExists)
        {
            return BadRequest("Category Id does not exist");
        }

        var transaction = new Transaction
        (
            Guid.NewGuid(),
            request.Amount,
            request.Description,
            request.Date,
            request.CategoryId
        );
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        var response = new TransactionResponse(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Date
        );

        return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTransaction(Guid id, UpdateTransactionRequest request)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        var categoryExists = await context.Categories.AnyAsync(c => c.Id == transaction.CategoryId);
        if (!categoryExists)
        {
            return BadRequest("Category Id doesn't exist");
        }

        transaction.UpdateDetails(
            request.Description,
            request.Amount,
            request.Date,
            request.CategoryId
        );

        await context.SaveChangesAsync();
        return NoContent();
    }
}