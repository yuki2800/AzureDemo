namespace InventoryManagement.Api.Infrastructure.Entities;

/// <summary>T_STOCK_TRANSACTIONテーブルに対応する永続化エンティティ。</summary>
public sealed class StockTransactionEntity
{
    public int StockTransactionId { get; set; }

    public int StockId { get; set; }

    public int ProductId { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public DateTime TransactionDatetime { get; set; }

    public DateTime CreatedDatetime { get; set; }

    public DateTime UpdatedDatetime { get; set; }
}
