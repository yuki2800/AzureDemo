namespace InventoryManagement.Api.Infrastructure.Entities;

/// <summary>M_PRODUCTテーブルに対応する永続化エンティティ。</summary>
public sealed class ProductEntity
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public DateTime CreatedDatetime { get; set; }

    public DateTime UpdatedDatetime { get; set; }
}
