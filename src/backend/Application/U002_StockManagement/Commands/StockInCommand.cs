namespace InventoryManagement.Api.Application.U002_StockManagement.Commands;

/// <summary>入庫コマンド。</summary>
public sealed record StockInCommand(int ProductId, decimal Quantity);
