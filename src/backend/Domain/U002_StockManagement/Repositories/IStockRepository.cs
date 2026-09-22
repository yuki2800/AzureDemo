using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;

namespace InventoryManagement.Api.Domain.U002_StockManagement.Repositories;

/// <summary>Stock集約のリポジトリインターフェース。</summary>
public interface IStockRepository
{
    Task<Stock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Stock>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Stock> SaveAsync(Stock stock, CancellationToken cancellationToken = default);
}
