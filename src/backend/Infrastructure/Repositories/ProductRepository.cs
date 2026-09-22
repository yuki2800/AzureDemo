using InventoryManagement.Api.Domain.U001_ProductManagement.Aggregates;
using InventoryManagement.Api.Domain.U001_ProductManagement.Repositories;
using InventoryManagement.Api.Domain.U001_ProductManagement.ValueObjects;
using InventoryManagement.Api.Domain.Shared.ValueObjects;
using InventoryManagement.Api.Infrastructure.Entities;
using InventoryManagement.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _dbContext;

    public ProductRepository(InventoryDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var entity = await this._dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);

        return entity is null ? null : ToDomain(entity);
    }

    public async Task<Product?> GetByCodeAsync(ProductCode productCode, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(productCode);

        var entity = await this._dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductCode == productCode.Value, cancellationToken);

        return entity is null ? null : ToDomain(entity);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await this._dbContext.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return entities.Select(ToDomain).ToList();
    }

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(product);

        var now = DateTime.Now;
        var entity = new ProductEntity
        {
            ProductCode = product.ProductCode.Value,
            ProductName = product.ProductName.Value,
            UnitPrice = product.UnitPrice.Amount,
            CreatedDatetime = now,
            UpdatedDatetime = now,
        };

        this._dbContext.Products.Add(entity);
        await this._dbContext.SaveChangesAsync(cancellationToken);

        return Product.Reconstruct(entity.ProductId, product.ProductCode, product.ProductName, product.UnitPrice);
    }

    private static Product ToDomain(ProductEntity entity)
    {
        return Product.Reconstruct(
            entity.ProductId,
            ProductCode.Of(entity.ProductCode),
            ProductName.Of(entity.ProductName),
            Money.Of(entity.UnitPrice));
    }
}
