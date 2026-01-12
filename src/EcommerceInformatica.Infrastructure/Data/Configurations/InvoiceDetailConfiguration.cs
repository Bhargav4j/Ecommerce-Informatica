using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        // Table mapping
        builder.ToTable("InvoiceDetails");

        // Primary key
        builder.HasKey(id => id.Id);

        // Property constraints
        builder.Property(id => id.Quantity)
            .IsRequired();

        builder.Property(id => id.UnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(id => id.Subtotal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(id => id.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(id => id.CreatedDate)
            .IsRequired();

        builder.Property(id => id.ModifiedDate);

        builder.Property(id => id.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(id => id.ModifiedBy)
            .HasMaxLength(100);

        // Foreign keys
        builder.Property(id => id.InvoiceId)
            .IsRequired();

        builder.Property(id => id.ProductId)
            .IsRequired();

        // Relationships
        builder.HasOne(id => id.Invoice)
            .WithMany(i => i.InvoiceDetails)
            .HasForeignKey(id => id.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(id => id.Product)
            .WithMany(p => p.InvoiceDetails)
            .HasForeignKey(id => id.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(id => id.InvoiceId);
        builder.HasIndex(id => id.ProductId);
    }
}
