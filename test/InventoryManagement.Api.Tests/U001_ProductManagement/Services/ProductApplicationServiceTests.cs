using InventoryManagement.Api.Application.U001_ProductManagement.Commands;
using InventoryManagement.Api.Application.U001_ProductManagement.Services;
using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;
using Moq;

namespace InventoryManagement.Api.Tests.U001_ProductManagement.Services;

public sealed class ProductApplicationServiceTests
{
    [Fact]
    public async Task RegisterProductAsync_商品コード重複時は例外がスローされる()
    {
        var productRepository = new Mock<IProductRepository>();
        productRepository
            .Setup(x => x.GetByCodeAsync(It.IsAny<ProductCode>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Product.Register(ProductCode.Of("ABC123"), ProductName.Of("既存商品"), Money.Of(500m)));

        var service = new ProductApplicationService(productRepository.Object);
        var command = new RegisterProductCommand("ABC123", "テスト商品", 1000m);

        await Assert.ThrowsAsync<ProductCodeDuplicateException>(() => service.RegisterProductAsync(command));
    }

    [Fact]
    public async Task RegisterProductAsync_正常登録時はIProductRepositoryのAddAsyncが呼ばれる()
    {
        var productRepository = new Mock<IProductRepository>();
        productRepository
            .Setup(x => x.GetByCodeAsync(It.IsAny<ProductCode>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);
        productRepository
            .Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product product, CancellationToken _) => product);

        var service = new ProductApplicationService(productRepository.Object);
        var command = new RegisterProductCommand("ABC123", "テスト商品", 1000m);

        await service.RegisterProductAsync(command);

        productRepository.Verify(
            x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
