using InventoryManagement.Api.Infrastructure.Entities;
using InventoryManagement.Api.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Infrastructure.Persistence;

public sealed class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    public DbSet<StockEntity> Stocks => Set<StockEntity>();

    public DbSet<StockTransactionEntity> StockTransactions => Set<StockTransactionEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StockEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StockTransactionEntityConfiguration());
    }
}
