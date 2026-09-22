namespace InventoryManagement.Api.Domain.U002_StockManagement.Exceptions;

/// <summary>出庫数量が現在庫数量を超える場合にスローされる。</summary>
public sealed class InsufficientStockException : Exception
{
    public InsufficientStockException(int productId)
        : base($"在庫が不足しています。ProductId: {productId}")
    {
    }
}
