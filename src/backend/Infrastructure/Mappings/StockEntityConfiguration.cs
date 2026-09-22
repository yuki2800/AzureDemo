using InventoryManagement.Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Api.Infrastructure.Mappings;

public sealed class StockEntityConfiguration : IEntityTypeConfiguration<StockEntity>
{
    public void Configure(EntityTypeBuilder<StockEntity> builder)
    {
        builder.ToTable("T_STOCK");
        builder.HasKey(x => x.StockId);

        builder.Property(x => x.StockId)
            .HasColumnName("stock_id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(10,3)")
            .IsRequired();

        builder.Property(x => x.CreatedDatetime)
            .HasColumnName("created_datetime")
            .IsRequired();

        builder.Property(x => x.UpdatedDatetime)
            .HasColumnName("updated_datetime")
            .IsRequired();

        builder.HasIndex(x => x.ProductId)
            .IsUnique();

        builder.HasOne<ProductEntity>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
