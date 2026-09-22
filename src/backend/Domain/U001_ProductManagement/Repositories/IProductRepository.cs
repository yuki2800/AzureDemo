using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;

namespace InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;

/// <summary>Product集約のリポジトリインターフェース。</summary>
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);

    Task<Product?> GetByCodeAsync(ProductCode productCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
}
