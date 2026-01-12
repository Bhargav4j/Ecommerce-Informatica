using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EcommerceInformatica.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
        modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Product || e.Entity is Category || e.Entity is Brand ||
                       e.Entity is Supplier || e.Entity is Person || e.Entity is Invoice ||
                       e.Entity is InvoiceDetail || e.Entity is PaymentMethod ||
                       e.Entity is Province || e.Entity is City);

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    SetAuditFieldsForAdd(entry);
                    break;
                case EntityState.Modified:
                    SetAuditFieldsForUpdate(entry);
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private static void SetAuditFieldsForAdd(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        var createdDateProperty = entry.Property("CreatedDate");
        var createdByProperty = entry.Property("CreatedBy");
        var isActiveProperty = entry.Property("IsActive");

        if (createdDateProperty != null)
            createdDateProperty.CurrentValue = DateTime.UtcNow;

        if (createdByProperty != null && string.IsNullOrEmpty(createdByProperty.CurrentValue?.ToString()))
            createdByProperty.CurrentValue = "System";

        if (isActiveProperty != null)
            isActiveProperty.CurrentValue = true;
    }

    private static void SetAuditFieldsForUpdate(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        var modifiedDateProperty = entry.Property("ModifiedDate");
        var modifiedByProperty = entry.Property("ModifiedBy");

        if (modifiedDateProperty != null)
            modifiedDateProperty.CurrentValue = DateTime.UtcNow;

        if (modifiedByProperty != null && string.IsNullOrEmpty(modifiedByProperty.CurrentValue?.ToString()))
            modifiedByProperty.CurrentValue = "System";
    }
}
