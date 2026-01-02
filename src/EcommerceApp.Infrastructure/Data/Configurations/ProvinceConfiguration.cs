using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Provincias");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("IdProvincia")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnName("NombreProvincia")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnName("Estado")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(100);
    }
}
