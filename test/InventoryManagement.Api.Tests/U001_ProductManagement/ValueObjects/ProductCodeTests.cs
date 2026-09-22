using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

namespace InventoryManagement.Api.Tests.U001_ProductManagement.ValueObjects;

public sealed class ProductCodeTests
{
    [Fact]
    public void Of_空文字の場合は例外がスローされる()
    {
        Assert.Throws<ArgumentException>(() => ProductCode.Of(""));
    }

    [Fact]
    public void Of_21文字以上の場合は例外がスローされる()
    {
        var value = new string('A', 21);

        Assert.Throws<ArgumentException>(() => ProductCode.Of(value));
    }

    [Fact]
    public void Of_英数字以外を含む場合は例外がスローされる()
    {
        Assert.Throws<ArgumentException>(() => ProductCode.Of("ABC-123"));
    }

    [Fact]
    public void Of_正常値の場合は生成できる()
    {
        var productCode = ProductCode.Of("ABC123");

        Assert.Equal("ABC123", productCode.Value);
    }
}
