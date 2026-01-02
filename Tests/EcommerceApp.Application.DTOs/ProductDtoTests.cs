using Xunit;
using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.DTOs.Tests;

public class ProductDtoTests
{
    [Fact]
    public void ProductDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new ProductDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.Price);
        Assert.Equal(0, dto.Stock);
        Assert.Null(dto.ImageUrl);
        Assert.Equal(0, dto.CategoryId);
        Assert.Equal(string.Empty, dto.CategoryName);
        Assert.Equal(0, dto.BrandId);
        Assert.Equal(string.Empty, dto.BrandName);
        Assert.Equal(0, dto.SupplierId);
        Assert.Equal(string.Empty, dto.SupplierName);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void ProductDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new ProductDto();

        // Act
        dto.Id = 1;
        dto.Name = "Laptop";
        dto.Description = "High-end laptop";
        dto.Price = 1299.99m;
        dto.Stock = 50;
        dto.ImageUrl = "laptop.jpg";
        dto.CategoryId = 1;
        dto.CategoryName = "Electronics";
        dto.BrandId = 2;
        dto.BrandName = "Dell";
        dto.SupplierId = 3;
        dto.SupplierName = "Tech Supplies";
        dto.IsActive = true;

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal("High-end laptop", dto.Description);
        Assert.Equal(1299.99m, dto.Price);
        Assert.Equal(50, dto.Stock);
        Assert.Equal("laptop.jpg", dto.ImageUrl);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal("Electronics", dto.CategoryName);
        Assert.Equal(2, dto.BrandId);
        Assert.Equal("Dell", dto.BrandName);
        Assert.Equal(3, dto.SupplierId);
        Assert.Equal("Tech Supplies", dto.SupplierName);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void ProductCreateDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new ProductCreateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.Price);
        Assert.Equal(0, dto.Stock);
        Assert.Null(dto.ImageUrl);
        Assert.Equal(0, dto.CategoryId);
        Assert.Equal(0, dto.BrandId);
        Assert.Equal(0, dto.SupplierId);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void ProductCreateDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new ProductCreateDto();

        // Act
        dto.Name = "Mouse";
        dto.Description = "Wireless mouse";
        dto.Price = 29.99m;
        dto.Stock = 100;
        dto.ImageUrl = "mouse.jpg";
        dto.CategoryId = 1;
        dto.BrandId = 2;
        dto.SupplierId = 3;
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Mouse", dto.Name);
        Assert.Equal("Wireless mouse", dto.Description);
        Assert.Equal(29.99m, dto.Price);
        Assert.Equal(100, dto.Stock);
        Assert.Equal("mouse.jpg", dto.ImageUrl);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal(2, dto.BrandId);
        Assert.Equal(3, dto.SupplierId);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void ProductUpdateDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new ProductUpdateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.Price);
        Assert.Equal(0, dto.Stock);
        Assert.Null(dto.ImageUrl);
        Assert.Equal(0, dto.CategoryId);
        Assert.Equal(0, dto.BrandId);
        Assert.Equal(0, dto.SupplierId);
        Assert.False(dto.IsActive);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void ProductUpdateDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new ProductUpdateDto();

        // Act
        dto.Name = "Updated Product";
        dto.Description = "Updated description";
        dto.Price = 99.99m;
        dto.Stock = 25;
        dto.ImageUrl = "updated.jpg";
        dto.CategoryId = 5;
        dto.BrandId = 6;
        dto.SupplierId = 7;
        dto.IsActive = true;
        dto.ModifiedBy = "System";

        // Assert
        Assert.Equal("Updated Product", dto.Name);
        Assert.Equal("Updated description", dto.Description);
        Assert.Equal(99.99m, dto.Price);
        Assert.Equal(25, dto.Stock);
        Assert.Equal("updated.jpg", dto.ImageUrl);
        Assert.Equal(5, dto.CategoryId);
        Assert.Equal(6, dto.BrandId);
        Assert.Equal(7, dto.SupplierId);
        Assert.True(dto.IsActive);
        Assert.Equal("System", dto.ModifiedBy);
    }
}
