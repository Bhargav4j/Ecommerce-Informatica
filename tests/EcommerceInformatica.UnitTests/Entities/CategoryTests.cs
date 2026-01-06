using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class CategoryTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var category = new Category();

        // Assert
        Assert.Equal(0, category.Id);
        Assert.Equal(string.Empty, category.Name);
        Assert.Null(category.Description);
        Assert.True(category.IsActive);
        Assert.InRange(category.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(category.ModifiedDate);
        Assert.Equal(string.Empty, category.CreatedBy);
        Assert.Null(category.ModifiedBy);
        Assert.NotNull(category.Products);
        Assert.Empty(category.Products);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Id = 100;

        // Assert
        Assert.Equal(100, category.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Name = "Electronics";

        // Assert
        Assert.Equal("Electronics", category.Name);
    }

    [Fact]
    public void Description_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Description = "Electronic devices";

        // Assert
        Assert.Equal("Electronic devices", category.Description);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();

        // Act
        category.IsActive = false;

        // Assert
        Assert.False(category.IsActive);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();
        var date = new DateTime(2024, 1, 1);

        // Act
        category.CreatedDate = date;

        // Assert
        Assert.Equal(date, category.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();
        var date = new DateTime(2024, 1, 2);

        // Act
        category.ModifiedDate = date;

        // Assert
        Assert.Equal(date, category.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();

        // Act
        category.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", category.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var category = new Category();

        // Act
        category.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", category.ModifiedBy);
    }

    [Fact]
    public void Products_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var category = new Category();

        // Assert
        Assert.NotNull(category.Products);
        Assert.Empty(category.Products);
        Assert.IsAssignableFrom<ICollection<Product>>(category.Products);
    }

    [Fact]
    public void Category_ShouldAllowProductsToBeAdded()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Electronics" };
        var product = new Product { Id = 1, Name = "Laptop", CategoryId = 1 };

        // Act
        category.Products.Add(product);

        // Assert
        Assert.Single(category.Products);
        Assert.Contains(product, category.Products);
    }

    [Fact]
    public void Category_ShouldHandleMultipleProducts()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Electronics" };
        var product1 = new Product { Id = 1, Name = "Laptop", CategoryId = 1 };
        var product2 = new Product { Id = 2, Name = "Mouse", CategoryId = 1 };

        // Act
        category.Products.Add(product1);
        category.Products.Add(product2);

        // Assert
        Assert.Equal(2, category.Products.Count);
        Assert.Contains(product1, category.Products);
        Assert.Contains(product2, category.Products);
    }
}
