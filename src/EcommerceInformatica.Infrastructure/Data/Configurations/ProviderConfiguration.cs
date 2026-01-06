using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Provider entity
/// </summary>
public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("Proveedores");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("IdProveedor");

        builder.Property(p => p.Name)
            .HasColumnName("NombreProveedor")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasMaxLength(100);

        builder.Property(p => p.Phone)
            .HasColumnName("Telefono")
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .HasColumnName("Direccion")
            .HasMaxLength(300);

        builder.Property(p => p.IsActive).HasColumnName("Estado").HasDefaultValue(true);
        builder.Property(p => p.CreatedDate).HasColumnName("FechaCreacion").HasDefaultValueSql("GETDATE()");
        builder.Property(p => p.ModifiedDate).HasColumnName("FechaModificacion");
        builder.Property(p => p.CreatedBy).HasColumnName("CreadoPor").IsRequired().HasMaxLength(100);
        builder.Property(p => p.ModifiedBy).HasColumnName("ModificadoPor").HasMaxLength(100);
    }
}
