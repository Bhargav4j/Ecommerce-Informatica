using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        // Table mapping
        builder.ToTable("PaymentMethods");

        // Primary key
        builder.HasKey(pm => pm.Id);

        // Property constraints
        builder.Property(pm => pm.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pm => pm.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(pm => pm.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(pm => pm.CreatedDate)
            .IsRequired();

        builder.Property(pm => pm.ModifiedDate);

        builder.Property(pm => pm.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pm => pm.ModifiedBy)
            .HasMaxLength(100);

        // Relationships
        builder.HasMany(pm => pm.Invoices)
            .WithOne(i => i.PaymentMethod)
            .HasForeignKey(i => i.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(pm => pm.Name)
            .IsUnique();
    }
}
