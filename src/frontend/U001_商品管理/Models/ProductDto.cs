using System.Text.Json.Serialization;

namespace InventoryManagement.Web.U001_ProductManagement.Models;

public sealed class ProductDto
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("productCode")]
    public string ProductCode { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }
}
