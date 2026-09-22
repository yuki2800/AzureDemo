namespace InventoryManagement.Api.Application.U001_ProductManagement.Commands;

/// <summary>商品登録コマンド。</summary>
public sealed record RegisterProductCommand(
    string ProductCode,
    string ProductName,
    decimal UnitPrice);
