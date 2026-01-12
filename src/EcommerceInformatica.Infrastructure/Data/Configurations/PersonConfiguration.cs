using EcommerceInformatica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceInformatica.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        // Table mapping
        builder.ToTable("Persons");

        // Primary key
        builder.HasKey(p => p.Id);

        // Property constraints
        builder.Property(p => p.Dni)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.IsAdmin)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.Property(p => p.ModifiedDate);

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(100);

        // Relationships
        builder.HasMany(p => p.Invoices)
            .WithOne(i => i.Person)
            .HasForeignKey(i => i.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(p => p.Dni)
            .IsUnique();

        builder.HasIndex(p => p.Email)
            .IsUnique();
    }
}
