using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class ProductUpdateDtoTests
{
    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = "Test Product";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void ProductUpdateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = "Test Description";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void ProductUpdateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetUnitPrice()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = 99.99m;

        // Act
        dto.UnitPrice = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.UnitPrice);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetStock()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = 100;

        // Act
        dto.Stock = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Stock);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetCategoryId()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = 1;

        // Act
        dto.CategoryId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CategoryId);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetBrandId()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = 1;

        // Act
        dto.BrandId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.BrandId);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetSupplierId()
    {
        // Arrange
        var dto = new ProductUpdateDto();
        var expectedValue = 1;

        // Act
        dto.SupplierId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.SupplierId);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new ProductUpdateDto();

        // Act
        dto.Name = "Laptop";
        dto.Description = "High-performance laptop";
        dto.UnitPrice = 1299.99m;
        dto.Stock = 50;
        dto.IsActive = true;
        dto.CategoryId = 10;
        dto.BrandId = 5;
        dto.SupplierId = 3;

        // Assert
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal("High-performance laptop", dto.Description);
        Assert.Equal(1299.99m, dto.UnitPrice);
        Assert.Equal(50, dto.Stock);
        Assert.True(dto.IsActive);
        Assert.Equal(10, dto.CategoryId);
        Assert.Equal(5, dto.BrandId);
        Assert.Equal(3, dto.SupplierId);
    }
}
