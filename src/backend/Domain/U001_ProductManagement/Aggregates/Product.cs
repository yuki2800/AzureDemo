using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

namespace InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;

/// <summary>商品集約。</summary>
public sealed class Product
{
    public int ProductId { get; private set; }

    public ProductCode ProductCode { get; }

    public ProductName ProductName { get; private set; }

    public Money UnitPrice { get; private set; }

    private Product(int productId, ProductCode productCode, ProductName productName, Money unitPrice)
    {
        ProductId = productId;
        ProductCode = productCode;
        ProductName = productName;
        UnitPrice = unitPrice;
    }

    /// <summary>新規商品を生成する。</summary>
    public static Product Register(ProductCode productCode, ProductName productName, Money unitPrice)
    {
        ArgumentNullException.ThrowIfNull(productCode);
        ArgumentNullException.ThrowIfNull(productName);
        ArgumentNullException.ThrowIfNull(unitPrice);

        return new Product(0, productCode, productName, unitPrice);
    }

    /// <summary>永続化データから商品集約を復元する。</summary>
    public static Product Reconstruct(int productId, ProductCode productCode, ProductName productName, Money unitPrice)
    {
        return new Product(productId, productCode, productName, unitPrice);
    }

    public void ChangePrice(Money newPrice)
    {
        ArgumentNullException.ThrowIfNull(newPrice);
        UnitPrice = newPrice;
    }

    /// <summary>永続化により採番されたIDをリポジトリから反映する。</summary>
    public void AssignProductId(int productId)
    {
        ProductId = productId;
    }
}
