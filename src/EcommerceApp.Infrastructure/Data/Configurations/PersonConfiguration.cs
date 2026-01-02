using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Personas");

        builder.HasKey(p => p.Dni);

        builder.Property(p => p.Dni)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Password)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.IsAdmin)
            .HasColumnName("Tipo")
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

        builder.HasIndex(p => p.Email).IsUnique();
    }
}
