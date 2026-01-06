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
        builder.ToTable("Articulos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("IdArticulo");

        builder.Property(p => p.Name)
            .HasColumnName("NombreArticulo")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasColumnName("Descripcion")
            .HasMaxLength(500);

        builder.Property(p => p.ProviderId).HasColumnName("IdProveedor");
        builder.Property(p => p.BrandId).HasColumnName("IdMarca");
        builder.Property(p => p.CategoryId).HasColumnName("IdCategoria");
        builder.Property(p => p.Stock).HasColumnName("Stock").HasDefaultValue(0);
        builder.Property(p => p.UnitPrice).HasColumnName("PrecioUnitario").HasPrecision(18, 2);
        builder.Property(p => p.IsActive).HasColumnName("Estado").HasDefaultValue(true);

        builder.Property(p => p.CreatedDate).HasColumnName("FechaCreacion").HasDefaultValueSql("GETDATE()");
        builder.Property(p => p.ModifiedDate).HasColumnName("FechaModificacion");
        builder.Property(p => p.CreatedBy).HasColumnName("CreadoPor").IsRequired().HasMaxLength(100);
        builder.Property(p => p.ModifiedBy).HasColumnName("ModificadoPor").HasMaxLength(100);

        // Relationships
        builder.HasOne(p => p.Provider)
            .WithMany(pr => pr.Products)
            .HasForeignKey(p => p.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
