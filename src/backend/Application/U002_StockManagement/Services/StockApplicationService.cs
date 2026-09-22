using InventoryManagement.Api.Application.U002_StockManagement.Commands;
using InventoryManagement.Api.Application.U002_StockManagement.Dtos;
using InventoryManagement.Api.Application.U002_StockManagement.Queries;
using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.DomainServices;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;

namespace InventoryManagement.Api.Application.U002_StockManagement.Services;

/// <summary>在庫管理ユースケースのアプリケーションサービス。</summary>
public sealed class StockApplicationService
{
    private readonly IStockRepository _stockRepository;
    private readonly IProductRepository _productRepository;
    private readonly IStockTransactionRepository _stockTransactionRepository;
    private readonly StockInboundService _stockInboundService;

    public StockApplicationService(
        IStockRepository stockRepository,
        IProductRepository productRepository,
        IStockTransactionRepository stockTransactionRepository,
        StockInboundService stockInboundService)
    {
        this._stockRepository = stockRepository;
        this._productRepository = productRepository;
        this._stockTransactionRepository = stockTransactionRepository;
        this._stockInboundService = stockInboundService;
    }

    public async Task<IReadOnlyList<StockDto>> GetStockListAsync(
        GetStockListQuery query,
        CancellationToken cancellationToken = default)
    {
        var products = await this._productRepository.GetAllAsync(cancellationToken);
        var stocks = await this._stockRepository.GetAllAsync(cancellationToken);
        var stockByProductId = stocks.ToDictionary(x => x.ProductId);

        var stockDtos = new List<StockDto>();
        foreach (var product in products)
        {
            var quantity = stockByProductId.TryGetValue(product.ProductId, out var stock)
                ? stock.Quantity.Value
                : 0m;
            var stockValue = quantity * product.UnitPrice.Amount;

            stockDtos.Add(new StockDto(
                product.ProductId,
                product.ProductCode.Value,
                product.ProductName.Value,
                quantity,
                product.UnitPrice.Amount,
                stockValue));
        }

        return stockDtos;
    }

    public async Task StockInAsync(StockInCommand command, CancellationToken cancellationToken = default)
    {
        var amount = Weight.Of(command.Quantity);
        await this._stockInboundService.StockInAsync(command.ProductId, amount, cancellationToken);
    }

    public async Task StockOutAsync(StockOutCommand command, CancellationToken cancellationToken = default)
    {
        var stock = await this._stockRepository.GetByProductIdAsync(command.ProductId, cancellationToken);
        if (stock is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }

        var amount = Weight.Of(command.Quantity);
        stock.StockOut(amount);
        var savedStock = await this._stockRepository.SaveAsync(stock, cancellationToken);

        var transaction = StockTransaction.Create(
            savedStock.StockId,
            command.ProductId,
            TransactionType.Out,
            amount,
            DateTime.Now);
        await this._stockTransactionRepository.AddAsync(transaction, cancellationToken);
    }
}
