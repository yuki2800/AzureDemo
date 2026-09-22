using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;
using InventoryManagement.Api.Infrastructure.Entities;
using InventoryManagement.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Infrastructure.Repositories;

public sealed class StockRepository : IStockRepository
{
    private readonly InventoryDbContext _dbContext;

    public StockRepository(InventoryDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Stock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var entity = await this._dbContext.Stocks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);

        return entity is null ? null : ToDomain(entity);
    }

    public async Task<IReadOnlyList<Stock>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await this._dbContext.Stocks
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return entities.Select(ToDomain).ToList();
    }

    public async Task<Stock> SaveAsync(Stock stock, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stock);

        var entity = await this._dbContext.Stocks
            .FirstOrDefaultAsync(x => x.ProductId == stock.ProductId, cancellationToken);
        var now = DateTime.Now;

        if (entity is null)
        {
            entity = new StockEntity
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity.Value,
                CreatedDatetime = now,
                UpdatedDatetime = now,
            };
            this._dbContext.Stocks.Add(entity);
        }
        else
        {
            entity.Quantity = stock.Quantity.Value;
            entity.UpdatedDatetime = now;
        }

        await this._dbContext.SaveChangesAsync(cancellationToken);

        return Stock.Reconstruct(entity.StockId, entity.ProductId, StockQuantity.Of(entity.Quantity));
    }

    private static Stock ToDomain(StockEntity entity)
    {
        return Stock.Reconstruct(entity.StockId, entity.ProductId, StockQuantity.Of(entity.Quantity));
    }
}
