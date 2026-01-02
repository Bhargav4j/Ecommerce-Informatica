using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Facturas");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasColumnName("IdFactura")
            .ValueGeneratedOnAdd();

        builder.Property(i => i.CustomerDni)
            .HasColumnName("DniCliente")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.InvoiceDate)
            .HasColumnName("FechaFactura")
            .IsRequired();

        builder.Property(i => i.Total)
            .HasColumnName("Total")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.PaymentMethodId)
            .HasColumnName("IdFormaPago")
            .IsRequired();

        builder.Property(i => i.IsActive)
            .HasColumnName("Estado")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(i => i.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(i => i.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(i => i.Customer)
            .WithMany(p => p.Invoices)
            .HasForeignKey(i => i.CustomerDni)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.PaymentMethod)
            .WithMany(pm => pm.Invoices)
            .HasForeignKey(i => i.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
