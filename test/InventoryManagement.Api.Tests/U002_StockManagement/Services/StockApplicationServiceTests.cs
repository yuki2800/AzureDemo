using InventoryManagement.Api.Application.U002_StockManagement.Commands;
using InventoryManagement.Api.Application.U002_StockManagement.Queries;
using InventoryManagement.Api.Application.U002_StockManagement.Services;
using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;
using InventoryManagement.Api.Domain.U002_StockManagement.Aggregates;
using InventoryManagement.Api.Domain.U002_StockManagement.DomainServices;
using InventoryManagement.Api.Domain.U002_StockManagement.Exceptions;
using InventoryManagement.Api.Domain.U002_StockManagement.Repositories;
using Moq;

namespace InventoryManagement.Api.Tests.U002_StockManagement.Services;

public sealed class StockApplicationServiceTests
{
    [Fact]
    public async Task GetStockListAsync_在庫金額が正しく計算される()
    {
        var product = Product.Register(ProductCode.Of("ABC123"), ProductName.Of("テスト商品"), Money.Of(100m));
        var productRepository = new Mock<IProductRepository>();
        productRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([product]);

        var stock = Stock.Create(product.ProductId);
        stock.StockIn(Weight.Of(5m));
        var stockRepository = new Mock<IStockRepository>();
        stockRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([stock]);

        var service = CreateService(stockRepository, productRepository, new Mock<IStockTransactionRepository>());

        var result = await service.GetStockListAsync(new GetStockListQuery());

        Assert.Equal(500m, result.Single().StockValue);
    }

    [Fact]
    public async Task StockOutAsync_在庫不足時は例外が伝播する()
    {
        var stock = Stock.Create(productId: 1);
        stock.StockIn(Weight.Of(5m));
        var stockRepository = new Mock<IStockRepository>();
        stockRepository
            .Setup(x => x.GetByProductIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(stock);

        var service = CreateService(stockRepository, new Mock<IProductRepository>(), new Mock<IStockTransactionRepository>());

        await Assert.ThrowsAsync<InsufficientStockException>(
            () => service.StockOutAsync(new StockOutCommand(1, 10m)));
    }

    private static StockApplicationService CreateService(
        Mock<IStockRepository> stockRepository,
        Mock<IProductRepository> productRepository,
        Mock<IStockTransactionRepository> stockTransactionRepository)
    {
        var stockInboundService = new StockInboundService(
            productRepository.Object,
            stockRepository.Object,
            stockTransactionRepository.Object);

        return new StockApplicationService(
            stockRepository.Object,
            productRepository.Object,
            stockTransactionRepository.Object,
            stockInboundService);
    }
}
