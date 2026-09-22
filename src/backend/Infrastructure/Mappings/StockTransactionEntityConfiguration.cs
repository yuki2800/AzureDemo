using InventoryManagement.Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Api.Infrastructure.Mappings;

public sealed class StockTransactionEntityConfiguration : IEntityTypeConfiguration<StockTransactionEntity>
{
    public void Configure(EntityTypeBuilder<StockTransactionEntity> builder)
    {
        builder.ToTable("T_STOCK_TRANSACTION");
        builder.HasKey(x => x.StockTransactionId);

        builder.Property(x => x.StockTransactionId)
            .HasColumnName("stock_transaction_id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.StockId)
            .HasColumnName("stock_id")
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(x => x.TransactionType)
            .HasColumnName("transaction_type")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(10,3)")
            .IsRequired();

        builder.Property(x => x.TransactionDatetime)
            .HasColumnName("transaction_datetime")
            .IsRequired();

        builder.Property(x => x.CreatedDatetime)
            .HasColumnName("created_datetime")
            .IsRequired();

        builder.Property(x => x.UpdatedDatetime)
            .HasColumnName("updated_datetime")
            .IsRequired();

        builder.HasOne<StockEntity>()
            .WithMany()
            .HasForeignKey(x => x.StockId);

        builder.HasOne<ProductEntity>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
