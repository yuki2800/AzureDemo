using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;
using InventoryManagement.Api.Infrastructure.Entities;
using InventoryManagement.Api.Infrastructure.Persistence;

namespace InventoryManagement.Api.Infrastructure.Repositories;

public sealed class StockTransactionRepository : IStockTransactionRepository
{
    private readonly InventoryDbContext _dbContext;

    public StockTransactionRepository(InventoryDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task AddAsync(StockTransaction stockTransaction, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stockTransaction);

        var now = DateTime.Now;
        var entity = new StockTransactionEntity
        {
            StockId = stockTransaction.StockId,
            ProductId = stockTransaction.ProductId,
            TransactionType = stockTransaction.TransactionType == TransactionType.In ? "IN" : "OUT",
            Quantity = stockTransaction.Quantity.Value,
            TransactionDatetime = stockTransaction.TransactionDatetime,
            CreatedDatetime = now,
            UpdatedDatetime = now,
        };

        this._dbContext.StockTransactions.Add(entity);
        await this._dbContext.SaveChangesAsync(cancellationToken);
    }
}
