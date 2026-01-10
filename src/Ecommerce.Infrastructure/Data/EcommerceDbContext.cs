using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    // public DbSet<Customer> Customers => Set<Customer>();
    // public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Person> Persons => Set<Person>();
    // public DbSet<Invoice> Invoices => Set<Invoice>();
    // public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
    // public DbSet<Shipper> Shippers => Set<Shipper>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        // modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        // modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        // modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        // modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
        // modelBuilder.ApplyConfiguration(new ShipperConfiguration());
    }
}
