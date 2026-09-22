using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;
using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.DomainServices;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using Moq;

namespace InventoryManagement.Api.Tests.U002_StockManagement.DomainServices;

public sealed class StockInboundServiceTests
{
    [Fact]
    public async Task StockInAsync_商品が存在しない場合は例外がスローされる()
    {
        var productRepository = new Mock<IProductRepository>();
        productRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);
        var stockRepository = new Mock<IStockRepository>();
        var stockTransactionRepository = new Mock<IStockTransactionRepository>();

        var service = new StockInboundService(
            productRepository.Object,
            stockRepository.Object,
            stockTransactionRepository.Object);

        await Assert.ThrowsAsync<ProductNotFoundException>(
            () => service.StockInAsync(1, Weight.Of(10m)));
    }

    [Fact]
    public async Task StockInAsync_商品が存在する場合はStockのStockInが呼ばれる()
    {
        var product = Product.Register(ProductCode.Of("ABC123"), ProductName.Of("テスト商品"), Money.Of(1000m));
        var productRepository = new Mock<IProductRepository>();
        productRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var stockRepository = new Mock<IStockRepository>();
        stockRepository
            .Setup(x => x.GetByProductIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Stock?)null);
        Stock? savedStock = null;
        stockRepository
            .Setup(x => x.SaveAsync(It.IsAny<Stock>(), It.IsAny<CancellationToken>()))
            .Callback<Stock, CancellationToken>((stock, _) => savedStock = stock)
            .ReturnsAsync((Stock stock, CancellationToken _) => stock);

        var stockTransactionRepository = new Mock<IStockTransactionRepository>();

        var service = new StockInboundService(
            productRepository.Object,
            stockRepository.Object,
            stockTransactionRepository.Object);

        await service.StockInAsync(1, Weight.Of(10m));

        Assert.NotNull(savedStock);
        Assert.Equal(10m, savedStock!.Quantity.Value);
    }
}
