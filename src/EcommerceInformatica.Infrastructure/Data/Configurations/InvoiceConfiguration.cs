using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        // Table mapping
        builder.ToTable("Invoices");

        // Primary key
        builder.HasKey(i => i.Id);

        // Property constraints
        builder.Property(i => i.InvoiceDate)
            .IsRequired();

        builder.Property(i => i.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(i => i.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(i => i.CreatedDate)
            .IsRequired();

        builder.Property(i => i.ModifiedDate);

        builder.Property(i => i.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.ModifiedBy)
            .HasMaxLength(100);

        // Foreign keys
        builder.Property(i => i.PersonId)
            .IsRequired();

        builder.Property(i => i.PaymentMethodId)
            .IsRequired();

        // Relationships
        builder.HasOne(i => i.Person)
            .WithMany(p => p.Invoices)
            .HasForeignKey(i => i.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.PaymentMethod)
            .WithMany(pm => pm.Invoices)
            .HasForeignKey(i => i.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(i => i.InvoiceDetails)
            .WithOne(id => id.Invoice)
            .HasForeignKey(id => id.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(i => i.InvoiceDate);
        builder.HasIndex(i => i.PersonId);
        builder.HasIndex(i => i.PaymentMethodId);
    }
}
