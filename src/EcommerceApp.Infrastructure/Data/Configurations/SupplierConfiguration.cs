using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Proveedores");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("IdProveedor")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .HasColumnName("Nombre")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.Address)
            .HasColumnName("Direccion")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(s => s.Phone)
            .HasColumnName("Telefono")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.Email)
            .HasColumnName("Email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(s => s.ContactPerson)
            .HasColumnName("PersonaContacto")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.CityId)
            .HasColumnName("IdCiudad")
            .IsRequired();

        builder.Property(s => s.IsActive)
            .HasColumnName("Estado")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(s => s.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(s => s.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(s => s.City)
            .WithMany(c => c.Suppliers)
            .HasForeignKey(s => s.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
