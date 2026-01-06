using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.DTOs.Tests;

/// <summary>
/// Test class for ProductDto, ProductCreateDto, and ProductUpdateDto
/// </summary>
public class ProductDtoTests
{
    [Fact]
    public void ProductDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new ProductDto
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            ProviderId = 1,
            ProviderName = "Test Provider",
            BrandId = 1,
            BrandName = "Test Brand",
            CategoryId = 1,
            CategoryName = "Test Category",
            Stock = 10,
            UnitPrice = 99.99m,
            IsActive = true
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Product", dto.Name);
        Assert.Equal("Test Description", dto.Description);
        Assert.Equal(1, dto.ProviderId);
        Assert.Equal("Test Provider", dto.ProviderName);
        Assert.Equal(1, dto.BrandId);
        Assert.Equal("Test Brand", dto.BrandName);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal("Test Category", dto.CategoryName);
        Assert.Equal(10, dto.Stock);
        Assert.Equal(99.99m, dto.UnitPrice);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void ProductDto_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var dto = new ProductDto
            {
                Name = null!
            };
        });
    }

    [Fact]
    public void ProductDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new ProductDto
        {
            Name = "Test Product",
            Description = null,
            ProviderName = null,
            BrandName = null,
            CategoryName = null
        };

        // Assert
        Assert.Null(dto.Description);
        Assert.Null(dto.ProviderName);
        Assert.Null(dto.BrandName);
        Assert.Null(dto.CategoryName);
    }

    [Fact]
    public void ProductCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new ProductCreateDto
        {
            Name = "New Product",
            Description = "New Description",
            ProviderId = 1,
            BrandId = 1,
            CategoryId = 1,
            Stock = 20,
            UnitPrice = 149.99m
        };

        // Assert
        Assert.Equal("New Product", dto.Name);
        Assert.Equal("New Description", dto.Description);
        Assert.Equal(1, dto.ProviderId);
        Assert.Equal(1, dto.BrandId);
        Assert.Equal(1, dto.CategoryId);
        Assert.Equal(20, dto.Stock);
        Assert.Equal(149.99m, dto.UnitPrice);
    }

    [Fact]
    public void ProductCreateDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new ProductCreateDto
        {
            Name = "New Product",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }

    [Fact]
    public void ProductUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new ProductUpdateDto
        {
            Name = "Updated Product",
            Description = "Updated Description",
            ProviderId = 2,
            BrandId = 2,
            CategoryId = 2,
            Stock = 30,
            UnitPrice = 199.99m,
            IsActive = false
        };

        // Assert
        Assert.Equal("Updated Product", dto.Name);
        Assert.Equal("Updated Description", dto.Description);
        Assert.Equal(2, dto.ProviderId);
        Assert.Equal(2, dto.BrandId);
        Assert.Equal(2, dto.CategoryId);
        Assert.Equal(30, dto.Stock);
        Assert.Equal(199.99m, dto.UnitPrice);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void ProductUpdateDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new ProductUpdateDto
        {
            Name = "Updated Product",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }
}
