using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Brand entity
/// </summary>
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Marcas");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("IdMarca");

        builder.Property(b => b.Name)
            .HasColumnName("NombreMarca")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .HasColumnName("Descripcion")
            .HasMaxLength(500);

        builder.Property(b => b.IsActive).HasColumnName("Estado").HasDefaultValue(true);
        builder.Property(b => b.CreatedDate).HasColumnName("FechaCreacion").HasDefaultValueSql("GETDATE()");
        builder.Property(b => b.ModifiedDate).HasColumnName("FechaModificacion");
        builder.Property(b => b.CreatedBy).HasColumnName("CreadoPor").IsRequired().HasMaxLength(100);
        builder.Property(b => b.ModifiedBy).HasColumnName("ModificadoPor").HasMaxLength(100);
    }
}
