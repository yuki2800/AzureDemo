using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

namespace InventoryManagement.Api.Tests.U001_ProductManagement.Aggregates;

public sealed class ProductTests
{
    [Fact]
    public void Register_正しく生成される()
    {
        var productCode = ProductCode.Of("ABC123");
        var productName = ProductName.Of("テスト商品");
        var unitPrice = Money.Of(1000m);

        var product = Product.Register(productCode, productName, unitPrice);

        Assert.Equal(productCode, product.ProductCode);
        Assert.Equal(productName, product.ProductName);
        Assert.Equal(unitPrice, product.UnitPrice);
    }

    [Fact]
    public void ChangePrice_単価が変更される()
    {
        var product = Product.Register(
            ProductCode.Of("ABC123"),
            ProductName.Of("テスト商品"),
            Money.Of(1000m));

        product.ChangePrice(Money.Of(2000m));

        Assert.Equal(2000m, product.UnitPrice.Amount);
    }
}
