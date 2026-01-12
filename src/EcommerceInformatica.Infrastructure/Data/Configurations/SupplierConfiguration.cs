using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        // Table mapping
        builder.ToTable("Suppliers");

        // Primary key
        builder.HasKey(s => s.Id);

        // Property constraints
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.ContactName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.ContactPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.ContactEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(s => s.CreatedDate)
            .IsRequired();

        builder.Property(s => s.ModifiedDate);

        builder.Property(s => s.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(100);

        // Relationships
        builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(s => s.Name);
        builder.HasIndex(s => s.ContactEmail);
    }
}
