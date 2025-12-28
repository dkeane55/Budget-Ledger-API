using BudgetOnline.Application.Contracts;
using BudgetOnline.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetOnline.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetTransactions()
    {
        var response = await _transactionService.GetAllTransactionsAsync();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponse>> CreateTransaction(CreateTransactionRequest request, CancellationToken ct)
    {
        var response = await _transactionService.CreateTransactionAsync(request, ct);

        if (response == null)
        {
            return BadRequest("Category Id does not exist");
        }

        return CreatedAtAction(nameof(GetTransactions), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTransaction(Guid id, UpdateTransactionRequest request, CancellationToken ct)
    {
        try
        {
            var transactionSuccess = await _transactionService.UpdateTransactionAsync(id, request, ct);
            if (!transactionSuccess)
            {
                return NotFound();
            }
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken ct)
    {
        var transactionSuccess = await _transactionService.DeleteTransactionAsync(id, ct);
        if (!transactionSuccess)
        {
            return NotFound();
        }

        return NoContent();
    }
}