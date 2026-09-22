namespace InventoryManagement.Api.Application.U002_StockManagement.Dtos;

/// <summary>在庫情報のDTO。</summary>
public sealed record StockDto(
    int ProductId,
    string ProductCode,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    decimal StockValue);
