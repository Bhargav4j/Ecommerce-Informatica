using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for InvoiceDetail entity
/// </summary>
public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.ToTable("InvoiceDetails");

        builder.HasKey(id => id.Id);

        builder.Property(id => id.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(id => id.Subtotal)
            .HasColumnType("decimal(18,2)");

        builder.Property(id => id.CreatedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(id => id.Invoice)
            .WithMany(i => i.InvoiceDetails)
            .HasForeignKey(id => id.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(id => id.Product)
            .WithMany(p => p.InvoiceDetails)
            .HasForeignKey(id => id.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
