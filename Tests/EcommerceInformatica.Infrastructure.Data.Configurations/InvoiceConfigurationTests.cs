using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Data.Configurations;

public class InvoiceConfigurationTests
{
    private ModelBuilder CreateModelBuilder()
    {
        var conventionSet = new ConventionSet();
        return new ModelBuilder(conventionSet);
    }

    [Fact]
    public void Configure_ShouldMapToInvoicesTable()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("Invoices", entityType.GetTableName());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var primaryKey = entityType.FindPrimaryKey();
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configure_ShouldSetInvoiceDatePropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var invoiceDateProperty = entityType.FindProperty("InvoiceDate");
        Assert.NotNull(invoiceDateProperty);
        Assert.False(invoiceDateProperty.IsNullable);
    }

    [Fact]
    public void Configure_ShouldSetTotalAmountPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var totalAmountProperty = entityType.FindProperty("TotalAmount");
        Assert.NotNull(totalAmountProperty);
        Assert.False(totalAmountProperty.IsNullable);
        Assert.Equal(18, totalAmountProperty.GetPrecision());
        Assert.Equal(2, totalAmountProperty.GetScale());
    }

    [Fact]
    public void Configure_ShouldSetIsActiveDefaultValue()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

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
        var configuration = new InvoiceConfiguration();

        // Act
        modelBuilder.Entity<Person>();
        modelBuilder.Entity<PaymentMethod>();
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var personIdProperty = entityType.FindProperty("PersonId");
        var paymentMethodIdProperty = entityType.FindProperty("PaymentMethodId");

        Assert.NotNull(personIdProperty);
        Assert.False(personIdProperty.IsNullable);
        Assert.NotNull(paymentMethodIdProperty);
        Assert.False(paymentMethodIdProperty.IsNullable);
    }

    [Fact]
    public void Configure_ShouldSetRelationships()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        modelBuilder.Entity<Person>();
        modelBuilder.Entity<PaymentMethod>();
        modelBuilder.Entity<InvoiceDetail>();
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var foreignKeys = entityType.GetForeignKeys().ToList();
        Assert.Equal(2, foreignKeys.Count);

        // Verify Person relationship
        var personFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Person));
        Assert.NotNull(personFk);
        Assert.Equal(DeleteBehavior.Restrict, personFk.DeleteBehavior);

        // Verify PaymentMethod relationship
        var paymentMethodFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(PaymentMethod));
        Assert.NotNull(paymentMethodFk);
        Assert.Equal(DeleteBehavior.Restrict, paymentMethodFk.DeleteBehavior);
    }

    [Fact]
    public void Configure_ShouldSetInvoiceDetailsRelationship()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        modelBuilder.Entity<Person>();
        modelBuilder.Entity<PaymentMethod>();
        modelBuilder.Entity<InvoiceDetail>();
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var navigation = entityType.FindNavigation("InvoiceDetails");
        Assert.NotNull(navigation);
        Assert.True(navigation.IsCollection);
    }

    [Fact]
    public void Configure_ShouldCreateIndexes()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        modelBuilder.Entity<Person>();
        modelBuilder.Entity<PaymentMethod>();
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

        // Assert
        Assert.NotNull(entityType);
        var indexes = entityType.GetIndexes().ToList();
        Assert.True(indexes.Count >= 3);

        // Verify indexes exist for InvoiceDate, PersonId, PaymentMethodId
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "InvoiceDate"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "PersonId"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "PaymentMethodId"));
    }

    [Fact]
    public void Configure_ShouldSetAuditFieldConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new InvoiceConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Invoice>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Invoice));

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
