using System.Text.Json.Serialization;

namespace InventoryManagement.Web.U002_StockManagement.Models;

public sealed class StockDto
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("productCode")]
    public string ProductCode { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }

    [JsonPropertyName("stockValue")]
    public decimal StockValue { get; set; }
}
