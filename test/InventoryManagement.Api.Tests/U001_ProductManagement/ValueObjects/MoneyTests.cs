using InventoryManagement.Api.Domain.Shared.ValueObjects;

namespace InventoryManagement.Api.Tests.U001_ProductManagement.ValueObjects;

public sealed class MoneyTests
{
    [Fact]
    public void Of_負数の場合は例外がスローされる()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Money.Of(-1m));
    }

    [Fact]
    public void Of_0の場合は許容される()
    {
        var money = Money.Of(0m);

        Assert.Equal(0m, money.Amount);
    }

    [Fact]
    public void Of_正常値の場合は生成できる()
    {
        var money = Money.Of(1000m);

        Assert.Equal(1000m, money.Amount);
    }
}
