using InventoryManagement.Api.Domain.U002_StockManagement.ValueObjects;

namespace InventoryManagement.Api.Tests.U002_StockManagement.ValueObjects;

public sealed class StockQuantityTests
{
    [Fact]
    public void Of_負数の場合は例外がスローされる()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => StockQuantity.Of(-1m));
    }

    [Fact]
    public void Of_0の場合は許容される()
    {
        var quantity = StockQuantity.Of(0m);

        Assert.Equal(0m, quantity.Value);
    }

    [Fact]
    public void Of_正常値の場合は生成できる()
    {
        var quantity = StockQuantity.Of(100m);

        Assert.Equal(100m, quantity.Value);
    }
}
