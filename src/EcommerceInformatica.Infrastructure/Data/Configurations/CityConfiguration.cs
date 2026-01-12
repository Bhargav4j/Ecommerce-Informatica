using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        // Table mapping
        builder.ToTable("Cities");

        // Primary key
        builder.HasKey(c => c.Id);

        // Property constraints
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.CreatedDate)
            .IsRequired();

        builder.Property(c => c.ModifiedDate);

        builder.Property(c => c.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ModifiedBy)
            .HasMaxLength(100);

        // Foreign keys
        builder.Property(c => c.ProvinceId)
            .IsRequired();

        // Relationships
        builder.HasOne(c => c.Province)
            .WithMany(p => p.Cities)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.ProvinceId);
    }
}
