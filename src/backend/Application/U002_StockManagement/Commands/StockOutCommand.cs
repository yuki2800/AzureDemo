namespace InventoryManagement.Api.Application.U002_StockManagement.Commands;

/// <summary>出庫コマンド。</summary>
public sealed record StockOutCommand(int ProductId, decimal Quantity);
