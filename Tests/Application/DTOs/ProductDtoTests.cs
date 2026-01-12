using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class ProductDtoTests
{
    [Fact]
    public void ProductDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "Test Product";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void ProductDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "Test Description";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void ProductDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetUnitPrice()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = 99.99m;

        // Act
        dto.UnitPrice = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.UnitPrice);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetStock()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = 100;

        // Act
        dto.Stock = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Stock);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void ProductDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void ProductDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProductDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void ProductDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetCategoryId()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = 1;

        // Act
        dto.CategoryId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CategoryId);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetCategoryName()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "Electronics";

        // Act
        dto.CategoryName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CategoryName);
    }

    [Fact]
    public void ProductDto_CategoryName_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.CategoryName = null;

        // Assert
        Assert.Null(dto.CategoryName);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetBrandId()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = 1;

        // Act
        dto.BrandId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.BrandId);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetBrandName()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "Samsung";

        // Act
        dto.BrandName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.BrandName);
    }

    [Fact]
    public void ProductDto_BrandName_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.BrandName = null;

        // Assert
        Assert.Null(dto.BrandName);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetSupplierId()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = 1;

        // Act
        dto.SupplierId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.SupplierId);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetSupplierName()
    {
        // Arrange
        var dto = new ProductDto();
        var expectedValue = "Tech Supplier Inc";

        // Act
        dto.SupplierName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.SupplierName);
    }

    [Fact]
    public void ProductDto_SupplierName_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.SupplierName = null;

        // Assert
        Assert.Null(dto.SupplierName);
    }

    [Fact]
    public void ProductDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new ProductDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "Laptop";
        dto.Description = "High-performance laptop";
        dto.UnitPrice = 1299.99m;
        dto.Stock = 50;
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";
        dto.CategoryId = 10;
        dto.CategoryName = "Computers";
        dto.BrandId = 5;
        dto.BrandName = "Dell";
        dto.SupplierId = 3;
        dto.SupplierName = "Tech Distributors";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal("High-performance laptop", dto.Description);
        Assert.Equal(1299.99m, dto.UnitPrice);
        Assert.Equal(50, dto.Stock);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
        Assert.Equal(10, dto.CategoryId);
        Assert.Equal("Computers", dto.CategoryName);
        Assert.Equal(5, dto.BrandId);
        Assert.Equal("Dell", dto.BrandName);
        Assert.Equal(3, dto.SupplierId);
        Assert.Equal("Tech Distributors", dto.SupplierName);
    }
}
