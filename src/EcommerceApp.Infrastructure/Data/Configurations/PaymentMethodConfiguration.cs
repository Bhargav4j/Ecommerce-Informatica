using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceApp.Infrastructure.Data.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("FormaPago");

        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.Id)
            .HasColumnName("IdFormaPago")
            .ValueGeneratedOnAdd();

        builder.Property(pm => pm.Name)
            .HasColumnName("NombreFormaPago")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(pm => pm.Description)
            .HasMaxLength(500);

        builder.Property(pm => pm.IsActive)
            .HasColumnName("Estado")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(pm => pm.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(pm => pm.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(pm => pm.ModifiedBy)
            .HasMaxLength(100);
    }
}
