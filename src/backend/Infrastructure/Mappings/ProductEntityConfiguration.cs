using InventoryManagement.Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Api.Infrastructure.Mappings;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("M_PRODUCT");
        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductCode)
            .HasColumnName("product_code")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.ProductName)
            .HasColumnName("product_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasColumnName("unit_price")
            .HasColumnType("decimal(12,2)")
            .IsRequired();

        builder.Property(x => x.CreatedDatetime)
            .HasColumnName("created_datetime")
            .IsRequired();

        builder.Property(x => x.UpdatedDatetime)
            .HasColumnName("updated_datetime")
            .IsRequired();

        builder.HasIndex(x => x.ProductCode)
            .IsUnique();
    }
}
