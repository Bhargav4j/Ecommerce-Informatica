using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Category entity
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("IdCategoria");

        builder.Property(c => c.Name)
            .HasColumnName("NombreCategoria")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasColumnName("Descripcion")
            .HasMaxLength(500);

        builder.Property(c => c.IsActive).HasColumnName("Estado").HasDefaultValue(true);
        builder.Property(c => c.CreatedDate).HasColumnName("FechaCreacion").HasDefaultValueSql("GETDATE()");
        builder.Property(c => c.ModifiedDate).HasColumnName("FechaModificacion");
        builder.Property(c => c.CreatedBy).HasColumnName("CreadoPor").IsRequired().HasMaxLength(100);
        builder.Property(c => c.ModifiedBy).HasColumnName("ModificadoPor").HasMaxLength(100);
    }
}
