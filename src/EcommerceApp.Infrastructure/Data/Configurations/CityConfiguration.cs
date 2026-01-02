using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Ciudades");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("IdCiudad")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasColumnName("NombreCiudad")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.ProvinceId)
            .HasColumnName("IdProvincia")
            .IsRequired();

        builder.Property(c => c.IsActive)
            .HasColumnName("Estado")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(c => c.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(c => c.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(c => c.Province)
            .WithMany(p => p.Cities)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
