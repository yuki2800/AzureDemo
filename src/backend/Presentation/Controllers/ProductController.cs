using InventoryManagement.Api.Application.U001_ProductManagement.Commands;
using InventoryManagement.Api.Application.U001_ProductManagement.Queries;
using InventoryManagement.Api.Application.U001_ProductManagement.Services;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductController : ControllerBase
{
    private readonly ProductApplicationService _productApplicationService;

    public ProductController(ProductApplicationService productApplicationService)
    {
        this._productApplicationService = productApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync(CancellationToken cancellationToken)
    {
        var products = await this._productApplicationService.GetProductListAsync(new GetProductListQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await this._productApplicationService.GetProductByIdAsync(new GetProductByIdQuery(id), cancellationToken);
            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
    }

    [HttpPost]
    public async Task<IActionResult> RegisterProductAsync(
        [FromBody] RegisterProductRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new RegisterProductCommand(request.ProductCode, request.ProductName, request.UnitPrice);
            var product = await this._productApplicationService.RegisterProductAsync(command, cancellationToken);
            // .NET 8+ の既定設定(SuppressAsyncSuffixInActionNames=true)により
            // 実際のアクション名は "Async" サフィックスなしの "GetProductById" となるため、
            // nameof(GetProductByIdAsync) ではなく文字列で指定する。
            return CreatedAtAction("GetProductById", new { id = product.ProductId }, product);
        }
        catch (Exception ex) when (ex is ArgumentException or ProductCodeDuplicateException)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
