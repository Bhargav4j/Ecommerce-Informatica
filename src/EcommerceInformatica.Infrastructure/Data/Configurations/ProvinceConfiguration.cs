using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        // Table mapping
        builder.ToTable("Provinces");

        // Primary key
        builder.HasKey(p => p.Id);

        // Property constraints
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.Property(p => p.ModifiedDate);

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(100);

        // Relationships
        builder.HasMany(p => p.Cities)
            .WithOne(c => c.Province)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}
