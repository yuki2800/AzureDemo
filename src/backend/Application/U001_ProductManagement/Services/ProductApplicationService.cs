using InventoryManagement.Api.Application.U001_ProductManagement.Commands;
using InventoryManagement.Api.Application.U001_ProductManagement.Dtos;
using InventoryManagement.Api.Application.U001_ProductManagement.Queries;
using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.Exceptions;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

namespace InventoryManagement.Api.Application.U001_ProductManagement.Services;

/// <summary>商品管理ユースケースのアプリケーションサービス。</summary>
public sealed class ProductApplicationService
{
    private readonly IProductRepository _productRepository;

    public ProductApplicationService(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }

    public async Task<IReadOnlyList<ProductDto>> GetProductListAsync(
        GetProductListQuery query,
        CancellationToken cancellationToken = default)
    {
        var products = await this._productRepository.GetAllAsync(cancellationToken);
        return products.Select(ToDto).ToList();
    }

    public async Task<ProductDto> GetProductByIdAsync(
        GetProductByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var product = await this._productRepository.GetByIdAsync(query.ProductId, cancellationToken);
        if (product is null)
        {
            throw new ArgumentException($"商品ID: {query.ProductId} は見つかりません。");
        }

        return ToDto(product);
    }

    public async Task<ProductDto> RegisterProductAsync(
        RegisterProductCommand command,
        CancellationToken cancellationToken = default)
    {
        var productCode = ProductCode.Of(command.ProductCode);

        var existingProduct = await this._productRepository.GetByCodeAsync(productCode, cancellationToken);
        if (existingProduct is not null)
        {
            throw new ProductCodeDuplicateException(command.ProductCode);
        }

        var productName = ProductName.Of(command.ProductName);
        var unitPrice = Money.Of(command.UnitPrice);

        var product = Product.Register(productCode, productName, unitPrice);
        var savedProduct = await this._productRepository.AddAsync(product, cancellationToken);

        return ToDto(savedProduct);
    }

    private static ProductDto ToDto(Product product)
    {
        return new ProductDto(
            product.ProductId,
            product.ProductCode.Value,
            product.ProductName.Value,
            product.UnitPrice.Amount);
    }
}
