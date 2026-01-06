using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Data;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Articulos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
            entity.Property(e => e.ImageUrl).HasMaxLength(200);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(8,2)");
            entity.HasOne(e => e.Supplier).WithMany(s => s.Products).HasForeignKey(e => e.SupplierId);
            entity.HasOne(e => e.Brand).WithMany(b => b.Products).HasForeignKey(e => e.BrandId);
            entity.HasOne(e => e.Category).WithMany(c => c.Products).HasForeignKey(e => e.CategoryId);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Personas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Dni).HasMaxLength(8).IsRequired();
            entity.HasIndex(e => e.Dni).IsUnique();
            entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(255).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Marcas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BrandId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ImageUrl).HasMaxLength(200);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categorias");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CategoryId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Provincias");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProvinceId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("Ciudades");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CityId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.HasOne(e => e.Province).WithMany(p => p.Cities).HasForeignKey(e => e.ProvinceId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Proveedores");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SupplierId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.BusinessName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Website).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.HasOne(e => e.Province).WithMany(p => p.Suppliers).HasForeignKey(e => e.ProvinceId);
            entity.HasOne(e => e.City).WithMany(c => c.Suppliers).HasForeignKey(e => e.CityId);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("FormaPago");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PaymentMethodId).HasMaxLength(4).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Facturas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InvoiceId).HasMaxLength(8).IsRequired();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10,2)");
            entity.HasOne(e => e.Person).WithMany(p => p.Invoices).HasForeignKey(e => e.PersonId);
            entity.HasOne(e => e.PaymentMethod).WithMany(pm => pm.Invoices).HasForeignKey(e => e.PaymentMethodId);
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.ToTable("DetalleFactura");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(8,2)");
            entity.HasOne(e => e.Invoice).WithMany(i => i.InvoiceDetails).HasForeignKey(e => e.InvoiceId);
            entity.HasOne(e => e.Product).WithMany(p => p.InvoiceDetails).HasForeignKey(e => e.ProductId);
        });
    }
}
