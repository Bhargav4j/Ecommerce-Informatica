using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.DTOs;

public class ProductDtoTests
{
    [Fact]
    public void ProductDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new ProductDto();

        // Assert
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.UnitPrice);
        Assert.Equal(0, dto.Stock);
        Assert.False(dto.IsActive);
        Assert.Equal(0, dto.CategoryId);
        Assert.Equal(string.Empty, dto.CategoryName);
        Assert.Equal(0, dto.BrandId);
        Assert.Equal(string.Empty, dto.BrandName);
        Assert.Equal(0, dto.SupplierId);
        Assert.Equal(string.Empty, dto.SupplierName);
    }

    [Fact]
    public void ProductDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.Id = 100;
        dto.Name = "Laptop";
        dto.Description = "Gaming laptop";
        dto.UnitPrice = 1500.50m;
        dto.Stock = 50;
        dto.IsActive = true;
        dto.CategoryId = 1;
        dto.CategoryName = "Electronics";
        dto.BrandId = 2;
        dto.BrandName = "Dell";
        dto.SupplierId = 3;
        dto.SupplierName = "Tech Supplier";
        dto.CreatedDate = new DateTime(2024, 1, 1);

        // Assert
        Assert.Equal(100, dto.Id);
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal("Gaming laptop", dto.Description);
        Assert.Equal(1500.50m, dto.UnitPrice);
        Assert.Equal(50, dto.Stock);
        Assert.True(dto.IsActive);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal("Electronics", dto.CategoryName);
        Assert.Equal(2, dto.BrandId);
        Assert.Equal("Dell", dto.BrandName);
        Assert.Equal(3, dto.SupplierId);
        Assert.Equal("Tech Supplier", dto.SupplierName);
        Assert.Equal(new DateTime(2024, 1, 1), dto.CreatedDate);
    }

    [Fact]
    public void ProductCreateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new ProductCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.UnitPrice);
        Assert.Equal(0, dto.Stock);
        Assert.Equal(0, dto.CategoryId);
        Assert.Equal(0, dto.BrandId);
        Assert.Equal(0, dto.SupplierId);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void ProductCreateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new ProductCreateDto();

        // Act
        dto.Name = "Laptop";
        dto.Description = "Gaming laptop";
        dto.UnitPrice = 1500.50m;
        dto.Stock = 50;
        dto.CategoryId = 1;
        dto.BrandId = 2;
        dto.SupplierId = 3;
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal("Gaming laptop", dto.Description);
        Assert.Equal(1500.50m, dto.UnitPrice);
        Assert.Equal(50, dto.Stock);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal(2, dto.BrandId);
        Assert.Equal(3, dto.SupplierId);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void ProductUpdateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new ProductUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.UnitPrice);
        Assert.Equal(0, dto.Stock);
        Assert.False(dto.IsActive);
        Assert.Equal(0, dto.CategoryId);
        Assert.Equal(0, dto.BrandId);
        Assert.Equal(0, dto.SupplierId);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void ProductUpdateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new ProductUpdateDto();

        // Act
        dto.Name = "Laptop Updated";
        dto.Description = "Updated gaming laptop";
        dto.UnitPrice = 1600.00m;
        dto.Stock = 40;
        dto.IsActive = true;
        dto.CategoryId = 1;
        dto.BrandId = 2;
        dto.SupplierId = 3;
        dto.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Laptop Updated", dto.Name);
        Assert.Equal("Updated gaming laptop", dto.Description);
        Assert.Equal(1600.00m, dto.UnitPrice);
        Assert.Equal(40, dto.Stock);
        Assert.True(dto.IsActive);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal(2, dto.BrandId);
        Assert.Equal(3, dto.SupplierId);
        Assert.Equal("Admin", dto.ModifiedBy);
    }
}
