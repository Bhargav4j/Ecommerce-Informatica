using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class BrandTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var brand = new Brand();

        // Assert
        Assert.Equal(0, brand.Id);
        Assert.Equal(string.Empty, brand.Name);
        Assert.Null(brand.Description);
        Assert.True(brand.IsActive);
        Assert.InRange(brand.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(brand.ModifiedDate);
        Assert.Equal(string.Empty, brand.CreatedBy);
        Assert.Null(brand.ModifiedBy);
        Assert.NotNull(brand.Products);
        Assert.Empty(brand.Products);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Id = 100;

        // Assert
        Assert.Equal(100, brand.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Name = "Samsung";

        // Assert
        Assert.Equal("Samsung", brand.Name);
    }

    [Fact]
    public void Description_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Description = "Electronics brand";

        // Assert
        Assert.Equal("Electronics brand", brand.Description);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.IsActive = false;

        // Assert
        Assert.False(brand.IsActive);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();
        var date = new DateTime(2024, 1, 1);

        // Act
        brand.CreatedDate = date;

        // Assert
        Assert.Equal(date, brand.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();
        var date = new DateTime(2024, 1, 2);

        // Act
        brand.ModifiedDate = date;

        // Assert
        Assert.Equal(date, brand.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", brand.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", brand.ModifiedBy);
    }

    [Fact]
    public void Products_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var brand = new Brand();

        // Assert
        Assert.NotNull(brand.Products);
        Assert.Empty(brand.Products);
        Assert.IsAssignableFrom<ICollection<Product>>(brand.Products);
    }

    [Fact]
    public void Brand_ShouldAllowProductsToBeAdded()
    {
        // Arrange
        var brand = new Brand { Id = 1, Name = "Samsung" };
        var product = new Product { Id = 1, Name = "Galaxy S24", BrandId = 1 };

        // Act
        brand.Products.Add(product);

        // Assert
        Assert.Single(brand.Products);
        Assert.Contains(product, brand.Products);
    }

    [Fact]
    public void Brand_ShouldHandleMultipleProducts()
    {
        // Arrange
        var brand = new Brand { Id = 1, Name = "Samsung" };
        var product1 = new Product { Id = 1, Name = "Galaxy S24", BrandId = 1 };
        var product2 = new Product { Id = 2, Name = "Galaxy Tab", BrandId = 1 };

        // Act
        brand.Products.Add(product1);
        brand.Products.Add(product2);

        // Assert
        Assert.Equal(2, brand.Products.Count);
        Assert.Contains(product1, brand.Products);
        Assert.Contains(product2, brand.Products);
    }
}
