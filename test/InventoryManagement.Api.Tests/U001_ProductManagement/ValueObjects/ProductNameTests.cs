using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

namespace InventoryManagement.Api.Tests.U001_ProductManagement.ValueObjects;

public sealed class ProductNameTests
{
    [Fact]
    public void Of_空文字の場合は例外がスローされる()
    {
        Assert.Throws<ArgumentException>(() => ProductName.Of(""));
    }

    [Fact]
    public void Of_101文字以上の場合は例外がスローされる()
    {
        var value = new string('あ', 101);

        Assert.Throws<ArgumentException>(() => ProductName.Of(value));
    }

    [Fact]
    public void Of_正常値の場合は生成できる()
    {
        var productName = ProductName.Of("テスト商品");

        Assert.Equal("テスト商品", productName.Value);
    }
}
