namespace InventoryManagement.Api.Application.U001_ProductManagement.Dtos;

/// <summary>商品情報のDTO。</summary>
public sealed record ProductDto(
    int ProductId,
    string ProductCode,
    string ProductName,
    decimal UnitPrice);
