using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;

namespace InventoryManagement.Api.Domain.U002_StockManagement.Repositories;

/// <summary>入出庫履歴の永続化を担うリポジトリインターフェース。</summary>
public interface IStockTransactionRepository
{
    Task AddAsync(StockTransaction stockTransaction, CancellationToken cancellationToken = default);
}
