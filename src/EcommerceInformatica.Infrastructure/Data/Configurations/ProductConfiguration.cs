using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Product entity
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Articulo");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("Id_Articulo_art");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("Nombre_Articulo_art");

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .HasColumnName("Descripcion_art");

        builder.Property(p => p.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .HasColumnName("Precio_Unitario_art");

        builder.Property(p => p.Stock)
            .HasColumnName("Stock_art");

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .HasColumnName("Estado_art");

        builder.Property(p => p.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("FechaCreacion_art");

        builder.Property(p => p.ModifiedDate)
            .HasColumnName("FechaModificacion_art");

        builder.Property(p => p.CreatedBy)
            .HasMaxLength(100)
            .HasColumnName("CreadoPor_art");

        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(100)
            .HasColumnName("ModificadoPor_art");

        builder.Property(p => p.CategoryId).HasColumnName("Id_Categoria_art");
        builder.Property(p => p.BrandId).HasColumnName("Id_Marca_art");
        builder.Property(p => p.SupplierId).HasColumnName("Id_Proveedor_art");

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
