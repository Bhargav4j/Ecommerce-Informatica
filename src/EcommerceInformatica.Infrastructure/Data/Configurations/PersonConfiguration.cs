using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Person entity
/// </summary>
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Personas");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("IdPersona");

        builder.Property(p => p.FirstName)
            .HasColumnName("Nombre")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .HasColumnName("Apellido")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Phone)
            .HasColumnName("Telefono")
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .HasColumnName("Direccion")
            .HasMaxLength(300);

        builder.Property(p => p.CityId).HasColumnName("IdCiudad");
        builder.Property(p => p.ProvinceId).HasColumnName("IdProvincia");

        builder.Property(p => p.Username)
            .HasColumnName("Usuario")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.PasswordHash)
            .HasColumnName("Contrasena")
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Role)
            .HasColumnName("Rol")
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.IsActive).HasColumnName("Estado").HasDefaultValue(true);
        builder.Property(p => p.CreatedDate).HasColumnName("FechaCreacion").HasDefaultValueSql("GETDATE()");
        builder.Property(p => p.ModifiedDate).HasColumnName("FechaModificacion");
        builder.Property(p => p.CreatedBy).HasColumnName("CreadoPor").IsRequired().HasMaxLength(100);
        builder.Property(p => p.ModifiedBy).HasColumnName("ModificadoPor").HasMaxLength(100);

        // Relationships
        builder.HasOne(p => p.City)
            .WithMany(c => c.Persons)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.Province)
            .WithMany(pr => pr.Persons)
            .HasForeignKey(p => p.ProvinceId)
            .OnDelete(DeleteBehavior.SetNull);

        // Index on username for login
        builder.HasIndex(p => p.Username).IsUnique();
    }
}
