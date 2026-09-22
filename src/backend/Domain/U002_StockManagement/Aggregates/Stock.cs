using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U002_StockManagement.Exceptions;
using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;

namespace InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;

/// <summary>在庫集約。</summary>
public sealed class Stock
{
    public int StockId { get; private set; }

    public int ProductId { get; }

    public StockQuantity Quantity { get; private set; }

    private Stock(int stockId, int productId, StockQuantity quantity)
    {
        StockId = stockId;
        ProductId = productId;
        Quantity = quantity;
    }

    /// <summary>対象商品の在庫レコードが未作成の場合に、数量0の在庫を新規生成する。</summary>
    public static Stock Create(int productId)
    {
        return new Stock(0, productId, StockQuantity.Zero);
    }

    /// <summary>永続化データから在庫集約を復元する。</summary>
    public static Stock Reconstruct(int stockId, int productId, StockQuantity quantity)
    {
        return new Stock(stockId, productId, quantity);
    }

    public void StockIn(Weight amount)
    {
        ArgumentNullException.ThrowIfNull(amount);
        Quantity = StockQuantity.Of(Quantity.Value + amount.Value);
    }

    public void StockOut(Weight amount)
    {
        ArgumentNullException.ThrowIfNull(amount);

        var remaining = Quantity.Value - amount.Value;
        if (remaining < 0)
        {
            throw new InsufficientStockException(ProductId);
        }

        Quantity = StockQuantity.Of(remaining);
    }

    /// <summary>永続化により採番されたIDをリポジトリから反映する。</summary>
    public void AssignStockId(int stockId)
    {
        StockId = stockId;
    }
}
