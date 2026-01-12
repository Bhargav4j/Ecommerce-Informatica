using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Data.Configurations;

public class InvoiceDetailConfigurationTests
{
    private ModelBuilder CreateModelBuilder()
    {
        var conventionSet = new ConventionSet();
        return new ModelBuilder(conventionSet);
    }

    [Fact]
    public void Configure_ShouldMapToInvoiceDetailsTable()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("InvoiceDetails", entityType.GetTableName());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var primaryKey = entityType.FindPrimaryKey();
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configure_ShouldSetQuantityPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var quantityProperty = entityType.FindProperty("Quantity");
        Assert.NotNull(quantityProperty);
        Assert.False(quantityProperty.IsNullable);
    }

    [Fact]
    public void Configure_ShouldSetUnitPricePropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var unitPriceProperty = entityType.FindProperty("UnitPrice");
        Assert.NotNull(unitPriceProperty);
        Assert.False(unitPriceProperty.IsNullable);
        Assert.Equal(18, unitPriceProperty.GetPrecision());
        Assert.Equal(2, unitPriceProperty.GetScale());
    }

    [Fact]
    public void Configure_ShouldSetSubtotalPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var subtotalProperty = entityType.FindProperty("Subtotal");
        Assert.NotNull(subtotalProperty);
        Assert.False(subtotalProperty.IsNullable);
        Assert.Equal(18, subtotalProperty.GetPrecision());
        Assert.Equal(2, subtotalProperty.GetScale());
    }

    [Fact]
    public void Configure_ShouldSetIsActiveDefaultValue()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var isActiveProperty = entityType.FindProperty("IsActive");
        Assert.NotNull(isActiveProperty);
        Assert.False(isActiveProperty.IsNullable);
        Assert.Equal(true, isActiveProperty.GetDefaultValue());
    }

    [Fact]
    public void Configure_ShouldSetForeignKeyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        modelBuilder.Entity<Invoice>();
        modelBuilder.Entity<Product>();
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var invoiceIdProperty = entityType.FindProperty("InvoiceId");
        var productIdProperty = entityType.FindProperty("ProductId");

        Assert.NotNull(invoiceIdProperty);
        Assert.False(invoiceIdProperty.IsNullable);
        Assert.NotNull(productIdProperty);
        Assert.False(productIdProperty.IsNullable);
    }

    [Fact]
    public void Configure_ShouldSetRelationships()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        modelBuilder.Entity<Invoice>();
        modelBuilder.Entity<Product>();
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var foreignKeys = entityType.GetForeignKeys().ToList();
        Assert.Equal(2, foreignKeys.Count);

        // Verify Invoice relationship with Cascade delete
        var invoiceFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Invoice));
        Assert.NotNull(invoiceFk);
        Assert.Equal(DeleteBehavior.Cascade, invoiceFk.DeleteBehavior);

        // Verify Product relationship with Restrict delete
        var productFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Product));
        Assert.NotNull(productFk);
        Assert.Equal(DeleteBehavior.Restrict, productFk.DeleteBehavior);
    }

    [Fact]
    public void Configure_ShouldCreateIndexes()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        modelBuilder.Entity<Invoice>();
        modelBuilder.Entity<Product>();
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);
        var indexes = entityType.GetIndexes().ToList();
        Assert.True(indexes.Count >= 2);

        // Verify indexes exist for InvoiceId and ProductId
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "InvoiceId"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "ProductId"));
    }

    [Fact]
    public void Configure_ShouldSetAuditFieldConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceDetailConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<InvoiceDetail>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(InvoiceDetail));

        // Assert
        Assert.NotNull(entityType);

        var createdByProperty = entityType.FindProperty("CreatedBy");
        Assert.NotNull(createdByProperty);
        Assert.False(createdByProperty.IsNullable);
        Assert.Equal(100, createdByProperty.GetMaxLength());

        var modifiedByProperty = entityType.FindProperty("ModifiedBy");
        Assert.NotNull(modifiedByProperty);
        Assert.True(modifiedByProperty.IsNullable);
        Assert.Equal(100, modifiedByProperty.GetMaxLength());

        var createdDateProperty = entityType.FindProperty("CreatedDate");
        Assert.NotNull(createdDateProperty);
        Assert.False(createdDateProperty.IsNullable);

        var modifiedDateProperty = entityType.FindProperty("ModifiedDate");
        Assert.NotNull(modifiedDateProperty);
        Assert.True(modifiedDateProperty.IsNullable);
    }
}
