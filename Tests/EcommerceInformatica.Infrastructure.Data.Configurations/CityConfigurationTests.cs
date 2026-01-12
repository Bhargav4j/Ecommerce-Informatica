using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Data.Configurations;

public class CityConfigurationTests
{
    private ModelBuilder CreateModelBuilder()
    {
        var conventionSet = new ConventionSet();
        return new ModelBuilder(conventionSet);
    }

    [Fact]
    public void Configure_ShouldMapToCitiesTable()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new CityConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("Cities", entityType.GetTableName());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new CityConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

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
        var configuration = new CityConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

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
        var configuration = new CityConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

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
        var configuration = new CityConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

        // Assert
        Assert.NotNull(entityType);
        var isActiveProperty = entityType.FindProperty("IsActive");
        Assert.NotNull(isActiveProperty);
        Assert.False(isActiveProperty.IsNullable);
        Assert.Equal(true, isActiveProperty.GetDefaultValue());
    }

    [Fact]
    public void Configure_ShouldSetProvinceIdForeignKeyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new CityConfiguration();

        // Act
        modelBuilder.Entity<Province>();
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

        // Assert
        Assert.NotNull(entityType);
        var provinceIdProperty = entityType.FindProperty("ProvinceId");
        Assert.NotNull(provinceIdProperty);
        Assert.False(provinceIdProperty.IsNullable);
    }

    [Fact]
    public void Configure_ShouldSetProvinceRelationship()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new CityConfiguration();

        // Act
        modelBuilder.Entity<Province>();
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

        // Assert
        Assert.NotNull(entityType);
        var foreignKeys = entityType.GetForeignKeys().ToList();
        Assert.Single(foreignKeys);

        var provinceFk = foreignKeys.First();
        Assert.Equal(typeof(Province), provinceFk.PrincipalEntityType.ClrType);
        Assert.Equal(DeleteBehavior.Restrict, provinceFk.DeleteBehavior);
    }

    [Fact]
    public void Configure_ShouldCreateIndexes()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new CityConfiguration();

        // Act
        modelBuilder.Entity<Province>();
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

        // Assert
        Assert.NotNull(entityType);
        var indexes = entityType.GetIndexes().ToList();
        Assert.True(indexes.Count >= 2);

        // Verify indexes exist for Name and ProvinceId
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "Name"));
        Assert.Contains(indexes, i => i.Properties.Any(p => p.Name == "ProvinceId"));
    }

    [Fact]
    public void Configure_ShouldSetAuditFieldConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new CityConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<City>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(City));

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
