using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;

namespace InventoryManagement.Api.Domain.U002_StockManagement.DomainServices;

/// <summary>
/// 入庫処理において、Product集約とStock集約の両方を参照する業務ロジックを担うドメインサービス。
/// </summary>
public sealed class StockInboundService
{
    private readonly IProductRepository _productRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IStockTransactionRepository _stockTransactionRepository;

    public StockInboundService(
        IProductRepository productRepository,
        IStockRepository stockRepository,
        IStockTransactionRepository stockTransactionRepository)
    {
        this._productRepository = productRepository;
        this._stockRepository = stockRepository;
        this._stockTransactionRepository = stockTransactionRepository;
    }

    public async Task StockInAsync(int productId, Weight amount, CancellationToken cancellationToken = default)
    {
        var product = await this._productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(productId);
        }

        var stock = await this._stockRepository.GetByProductIdAsync(productId, cancellationToken)
            ?? Stock.Create(productId);
        stock.StockIn(amount);
        var savedStock = await this._stockRepository.SaveAsync(stock, cancellationToken);

        var transaction = StockTransaction.Create(
            savedStock.StockId,
            productId,
            TransactionType.In,
            amount,
            DateTime.Now);
        await this._stockTransactionRepository.AddAsync(transaction, cancellationToken);
    }
}
