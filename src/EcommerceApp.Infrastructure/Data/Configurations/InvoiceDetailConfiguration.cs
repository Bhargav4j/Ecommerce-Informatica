using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.ToTable("DetalleFactura");

        builder.HasKey(d => new { d.InvoiceId, d.ProductId });

        builder.Property(d => d.InvoiceId)
            .HasColumnName("IdFactura")
            .IsRequired();

        builder.Property(d => d.ProductId)
            .HasColumnName("IdProducto")
            .IsRequired();

        builder.Property(d => d.Quantity)
            .HasColumnName("Cantidad")
            .IsRequired();

        builder.Property(d => d.UnitPrice)
            .HasColumnName("PrecioUnitario")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(d => d.Subtotal)
            .HasColumnName("Subtotal")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(d => d.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(d => d.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(d => d.Invoice)
            .WithMany(i => i.InvoiceDetails)
            .HasForeignKey(d => d.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Product)
            .WithMany(p => p.InvoiceDetails)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
