using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Productos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("IdProducto")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnName("NombreProducto")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("Descripcion")
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasColumnName("Precio")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Stock)
            .HasColumnName("Stock")
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .HasColumnName("Imagen")
            .HasMaxLength(500);

        builder.Property(p => p.CategoryId)
            .HasColumnName("IdCategoria")
            .IsRequired();

        builder.Property(p => p.BrandId)
            .HasColumnName("IdMarca")
            .IsRequired();

        builder.Property(p => p.SupplierId)
            .HasColumnName("IdProveedor")
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
