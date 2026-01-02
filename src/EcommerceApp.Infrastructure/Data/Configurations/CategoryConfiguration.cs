using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("IdCategoria")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasColumnName("NombreCategoria")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

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
    }
}
