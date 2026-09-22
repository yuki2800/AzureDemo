using InventoryManagement.Api.Application.U002_StockManagement.Commands;
using InventoryManagement.Api.Application.U002_StockManagement.Queries;
using InventoryManagement.Api.Application.U002_StockManagement.Services;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Domain.U002_StockManagement.Exceptions;
using InventoryManagement.Api.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Presentation.Controllers;

[ApiController]
[Route("api/stocks")]
public sealed class StockController : ControllerBase
{
    private readonly StockApplicationService _stockApplicationService;

    public StockController(StockApplicationService stockApplicationService)
    {
        this._stockApplicationService = stockApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStocksAsync(CancellationToken cancellationToken)
    {
        var stocks = await this._stockApplicationService.GetStockListAsync(new GetStockListQuery(), cancellationToken);
        return Ok(stocks);
    }

    [HttpPost("transactions")]
    public async Task<IActionResult> RegisterTransactionAsync(
        [FromBody] StockTransactionRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.Equals(request.TransactionType, "IN", StringComparison.OrdinalIgnoreCase))
            {
                var command = new StockInCommand(request.ProductId, request.Quantity);
                await this._stockApplicationService.StockInAsync(command, cancellationToken);
            }
            else if (string.Equals(request.TransactionType, "OUT", StringComparison.OrdinalIgnoreCase))
            {
                var command = new StockOutCommand(request.ProductId, request.Quantity);
                await this._stockApplicationService.StockOutAsync(command, cancellationToken);
            }
            else
            {
                return Problem(detail: "入出庫区分はINまたはOUTを指定してください。", statusCode: StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex) when (ex is ArgumentException or ProductNotFoundException or InsufficientStockException)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
