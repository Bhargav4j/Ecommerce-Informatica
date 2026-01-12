using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Data.Configurations;

public class PaymentMethodConfigurationTests
{
    private ModelBuilder CreateModelBuilder()
    {
        var conventionSet = new ConventionSet();
        return new ModelBuilder(conventionSet);
    }

    [Fact]
    public void Configure_ShouldMapToPaymentMethodsTable()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("PaymentMethods", entityType.GetTableName());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        var primaryKey = entityType.FindPrimaryKey();
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configure_ShouldSetNamePropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        var nameProperty = entityType.FindProperty("Name");
        Assert.NotNull(nameProperty);
        Assert.False(nameProperty.IsNullable);
        Assert.Equal(100, nameProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetDescriptionPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        var descriptionProperty = entityType.FindProperty("Description");
        Assert.NotNull(descriptionProperty);
        Assert.False(descriptionProperty.IsNullable);
        Assert.Equal(500, descriptionProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetIsActiveDefaultValue()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        var isActiveProperty = entityType.FindProperty("IsActive");
        Assert.NotNull(isActiveProperty);
        Assert.False(isActiveProperty.IsNullable);
        Assert.Equal(true, isActiveProperty.GetDefaultValue());
    }

    [Fact]
    public void Configure_ShouldCreateUniqueIndexOnName()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        var indexes = entityType.GetIndexes().ToList();
        var nameIndex = indexes.FirstOrDefault(i => i.Properties.Any(p => p.Name == "Name"));
        Assert.NotNull(nameIndex);
        Assert.True(nameIndex.IsUnique);
    }

    [Fact]
    public void Configure_ShouldSetAuditFieldConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

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

    [Fact]
    public void Configure_ShouldSetInvoicesRelationship()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PaymentMethodConfiguration();

        // Act
        modelBuilder.Entity<Invoice>();
        configuration.Configure(modelBuilder.Entity<PaymentMethod>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(PaymentMethod));

        // Assert
        Assert.NotNull(entityType);
        var navigation = entityType.FindNavigation("Invoices");
        Assert.NotNull(navigation);
        Assert.True(navigation.IsCollection);
    }
}
