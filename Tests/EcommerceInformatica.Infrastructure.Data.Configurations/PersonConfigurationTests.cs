using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Data.Configurations;

public class PersonConfigurationTests
{
    private ModelBuilder CreateModelBuilder()
    {
        var conventionSet = new ConventionSet();
        return new ModelBuilder(conventionSet);
    }

    [Fact]
    public void Configure_ShouldMapToPersonsTable()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("Persons", entityType.GetTableName());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var primaryKey = entityType.FindPrimaryKey();
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configure_ShouldSetDniPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var dniProperty = entityType.FindProperty("Dni");
        Assert.NotNull(dniProperty);
        Assert.False(dniProperty.IsNullable);
        Assert.Equal(20, dniProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetNamePropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);

        var firstNameProperty = entityType.FindProperty("FirstName");
        Assert.NotNull(firstNameProperty);
        Assert.False(firstNameProperty.IsNullable);
        Assert.Equal(100, firstNameProperty.GetMaxLength());

        var lastNameProperty = entityType.FindProperty("LastName");
        Assert.NotNull(lastNameProperty);
        Assert.False(lastNameProperty.IsNullable);
        Assert.Equal(100, lastNameProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetContactPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);

        var emailProperty = entityType.FindProperty("Email");
        Assert.NotNull(emailProperty);
        Assert.False(emailProperty.IsNullable);
        Assert.Equal(100, emailProperty.GetMaxLength());

        var phoneProperty = entityType.FindProperty("Phone");
        Assert.NotNull(phoneProperty);
        Assert.False(phoneProperty.IsNullable);
        Assert.Equal(20, phoneProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetPasswordHashPropertyConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var passwordHashProperty = entityType.FindProperty("PasswordHash");
        Assert.NotNull(passwordHashProperty);
        Assert.False(passwordHashProperty.IsNullable);
        Assert.Equal(500, passwordHashProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetIsAdminDefaultValue()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var isAdminProperty = entityType.FindProperty("IsAdmin");
        Assert.NotNull(isAdminProperty);
        Assert.False(isAdminProperty.IsNullable);
        Assert.Equal(false, isAdminProperty.GetDefaultValue());
    }

    [Fact]
    public void Configure_ShouldSetIsActiveDefaultValue()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var isActiveProperty = entityType.FindProperty("IsActive");
        Assert.NotNull(isActiveProperty);
        Assert.False(isActiveProperty.IsNullable);
        Assert.Equal(true, isActiveProperty.GetDefaultValue());
    }

    [Fact]
    public void Configure_ShouldCreateUniqueIndexes()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var indexes = entityType.GetIndexes().ToList();

        var dniIndex = indexes.FirstOrDefault(i => i.Properties.Any(p => p.Name == "Dni"));
        Assert.NotNull(dniIndex);
        Assert.True(dniIndex.IsUnique);

        var emailIndex = indexes.FirstOrDefault(i => i.Properties.Any(p => p.Name == "Email"));
        Assert.NotNull(emailIndex);
        Assert.True(emailIndex.IsUnique);
    }

    [Fact]
    public void Configure_ShouldSetAuditFieldConstraints()
    {
        // Arrange
        var modelBuilder = CreateModelBuilder();
        var configuration = new PersonConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

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
        var configuration = new PersonConfiguration();

        // Act
        modelBuilder.Entity<Invoice>();
        configuration.Configure(modelBuilder.Entity<Person>());
        var model = modelBuilder.FinalizeModel();
        var entityType = model.FindEntityType(typeof(Person));

        // Assert
        Assert.NotNull(entityType);
        var navigation = entityType.FindNavigation("Invoices");
        Assert.NotNull(navigation);
        Assert.True(navigation.IsCollection);
    }
}
