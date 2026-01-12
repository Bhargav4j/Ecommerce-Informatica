using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Data.Configurations;

public class ProductConfigurationTests
{
    private ModelBuilder CreateModelBuilder()
    {
        var conventionSet = new ConventionSet();
        return new ModelBuilder(conventionSet);
    }

    [Fact]
    public void Configure_ShouldMapToProductsTable()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("Products", entityType.GetTableName());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

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
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

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
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        var descriptionProperty = entityType.FindProperty("Description");
        Assert.NotNull(descriptionProperty);
        Assert.False(descriptionProperty.IsNullable);
        Assert.Equal(500, descriptionProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetUnitPricePropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        var unitPriceProperty = entityType.FindProperty("UnitPrice");
        Assert.NotNull(unitPriceProperty);
        Assert.False(unitPriceProperty.IsNullable);
        Assert.Equal(18, unitPriceProperty.GetPrecision());
        Assert.Equal(2, unitPriceProperty.GetScale());
    }

    [Fact]
    public void Configure_ShouldSetIsActiveDefaultValue()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

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
        var configuration = new ProductConfiguration();

        // Act
        modelBuilder.Entity<Category>();
        modelBuilder.Entity<Brand>();
        modelBuilder.Entity<Supplier>();
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        var categoryIdProperty = entityType.FindProperty("CategoryId");
        var brandIdProperty = entityType.FindProperty("BrandId");
        var supplierIdProperty = entityType.FindProperty("SupplierId");

        Assert.NotNull(categoryIdProperty);
        Assert.False(categoryIdProperty.IsNullable);
        Assert.NotNull(brandIdProperty);
        Assert.False(brandIdProperty.IsNullable);
        Assert.NotNull(supplierIdProperty);
        Assert.False(supplierIdProperty.IsNullable);
    }

    [Fact]
    public void Configure_ShouldSetRelationships()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        modelBuilder.Entity<Category>();
        modelBuilder.Entity<Brand>();
        modelBuilder.Entity<Supplier>();
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        var foreignKeys = entityType.GetForeignKeys().ToList();
        Assert.Equal(3, foreignKeys.Count);

        // Verify Category relationship
        var categoryFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Category));
        Assert.NotNull(categoryFk);
        Assert.Equal(DeleteBehavior.Restrict, categoryFk.DeleteBehavior);

        // Verify Brand relationship
        var brandFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Brand));
        Assert.NotNull(brandFk);
        Assert.Equal(DeleteBehavior.Restrict, brandFk.DeleteBehavior);

        // Verify Supplier relationship
        var supplierFk = foreignKeys.FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Supplier));
        Assert.NotNull(supplierFk);
        Assert.Equal(DeleteBehavior.Restrict, supplierFk.DeleteBehavior);
    }

    [Fact]
    public void Configure_ShouldCreateIndexes()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        modelBuilder.Entity<Category>();
        modelBuilder.Entity<Brand>();
        modelBuilder.Entity<Supplier>();
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        var indexes = entityType.GetIndexes().ToList();
        Assert.True(indexes.Count >= 4);

        // Verify indexes exist for Name, CategoryId, BrandId, SupplierId
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "Name"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "CategoryId"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "BrandId"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "SupplierId"));
    }

    [Fact]
    public void Configure_ShouldSetAuditFieldConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new ProductConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Product>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Product));

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
