using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.Exceptions;

namespace InventoryManagement.Api.Tests.U002_StockManagement.Aggregates;

public sealed class StockTests
{
    [Fact]
    public void StockIn_数量が加算される()
    {
        var stock = Stock.Create(productId: 1);

        stock.StockIn(Weight.Of(10m));

        Assert.Equal(10m, stock.Quantity.Value);
    }

    [Fact]
    public void StockOut_在庫不足時は例外がスローされる()
    {
        var stock = Stock.Create(productId: 1);
        stock.StockIn(Weight.Of(5m));

        Assert.Throws<InsufficientStockException>(() => stock.StockOut(Weight.Of(10m)));
    }

    [Fact]
    public void StockOut_正常時は数量が減算される()
    {
        var stock = Stock.Create(productId: 1);
        stock.StockIn(Weight.Of(10m));

        stock.StockOut(Weight.Of(4m));

        Assert.Equal(6m, stock.Quantity.Value);
    }
}
