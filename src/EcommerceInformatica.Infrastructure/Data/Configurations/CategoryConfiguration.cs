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
        builder.ToTable("Categoria");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id_Categoria_cat");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("Nombre_Categoria_cat");

        builder.Property(c => c.Description)
            .HasMaxLength(500)
            .HasColumnName("Descripcion_cat");

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true)
            .HasColumnName("Estado_cat");

        builder.Property(c => c.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("FechaCreacion_cat");

        builder.Property(c => c.ModifiedDate)
            .HasColumnName("FechaModificacion_cat");

        builder.Property(c => c.CreatedBy)
            .HasMaxLength(100)
            .HasColumnName("CreadoPor_cat");

        builder.Property(c => c.ModifiedBy)
            .HasMaxLength(100)
            .HasColumnName("ModificadoPor_cat");

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
