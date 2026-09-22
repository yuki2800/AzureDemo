using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.Presentation.Requests;

/// <summary>入出庫登録リクエスト。</summary>
public sealed class StockTransactionRequest
{
    public int ProductId { get; set; }

    [Required]
    public string TransactionType { get; set; } = string.Empty;

    [Range(0.001, double.MaxValue)]
    public decimal Quantity { get; set; }
}
