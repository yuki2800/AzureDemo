namespace InventoryManagement.Api.Infrastructure.Entities;

/// <summary>T_STOCKテーブルに対応する永続化エンティティ。</summary>
public sealed class StockEntity
{
    public int StockId { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedDatetime { get; set; }

    public DateTime UpdatedDatetime { get; set; }
}
