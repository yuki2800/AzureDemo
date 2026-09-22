using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.Presentation.Requests;

/// <summary>商品登録リクエスト。</summary>
public sealed class RegisterProductRequest
{
    [Required]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}
