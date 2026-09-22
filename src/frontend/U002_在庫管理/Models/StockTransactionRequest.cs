using System.Text.Json.Serialization;

namespace InventoryManagement.Web.U002_StockManagement.Models;

public sealed class StockTransactionRequest
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("transactionType")]
    public string TransactionType { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }
}
