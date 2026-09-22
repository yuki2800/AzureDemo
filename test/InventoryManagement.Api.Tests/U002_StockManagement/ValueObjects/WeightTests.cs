using InventoryManagement.Api.Domain.Shared.ValueObjects;

namespace InventoryManagement.Api.Tests.U002_StockManagement.ValueObjects;

public sealed class WeightTests
{
    [Fact]
    public void Of_0以下の場合は例外がスローされる()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Weight.Of(0m));
    }

    [Fact]
    public void Of_正常値の場合は生成できる()
    {
        var weight = Weight.Of(10m);

        Assert.Equal(10m, weight.Value);
    }
}
