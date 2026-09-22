using System.Text.Json.Serialization;

namespace InventoryManagement.Web.U001_ProductManagement.Models;

public sealed class RegisterProductRequest
{
    [JsonPropertyName("productCode")]
    public string ProductCode { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }
}
