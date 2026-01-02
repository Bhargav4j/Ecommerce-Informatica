using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Marcas");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("IdMarca")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .HasColumnName("NombreMarca")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(500);

        builder.Property(b => b.IsActive)
            .HasColumnName("Estado")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(b => b.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(b => b.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.ModifiedBy)
            .HasMaxLength(100);
    }
}
