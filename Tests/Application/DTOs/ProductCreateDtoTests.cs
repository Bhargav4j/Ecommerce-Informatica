using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class ProductCreateDtoTests
{
    [Fact]
    public void ProductCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = "Test Product";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void ProductCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = "Test Description";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void ProductCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetUnitPrice()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = 99.99m;

        // Act
        dto.UnitPrice = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.UnitPrice);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetStock()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = 100;

        // Act
        dto.Stock = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Stock);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetCategoryId()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = 1;

        // Act
        dto.CategoryId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CategoryId);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetBrandId()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = 1;

        // Act
        dto.BrandId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.BrandId);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetSupplierId()
    {
        // Arrange
        var dto = new ProductCreateDto();
        var expectedValue = 1;

        // Act
        dto.SupplierId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.SupplierId);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new ProductCreateDto();

        // Act
        dto.Name = "Laptop";
        dto.Description = "High-performance laptop";
        dto.UnitPrice = 1299.99m;
        dto.Stock = 50;
        dto.CategoryId = 10;
        dto.BrandId = 5;
        dto.SupplierId = 3;

        // Assert
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal("High-performance laptop", dto.Description);
        Assert.Equal(1299.99m, dto.UnitPrice);
        Assert.Equal(50, dto.Stock);
        Assert.Equal(10, dto.CategoryId);
        Assert.Equal(5, dto.BrandId);
        Assert.Equal(3, dto.SupplierId);
    }
}
