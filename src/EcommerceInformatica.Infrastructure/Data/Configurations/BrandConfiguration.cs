using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        // Table mapping
        builder.ToTable("Brands");

        // Primary key
        builder.HasKey(b => b.Id);

        // Property constraints
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(b => b.CreatedDate)
            .IsRequired();

        builder.Property(b => b.ModifiedDate);

        builder.Property(b => b.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.ModifiedBy)
            .HasMaxLength(100);

        // Relationships
        builder.HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(b => b.Name)
            .IsUnique();
    }
}
