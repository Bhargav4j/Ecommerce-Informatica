using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.DTOs.Tests;

/// <summary>
/// Test class for BrandDto, BrandCreateDto, and BrandUpdateDto
/// </summary>
public class BrandDtoTests
{
    [Fact]
    public void BrandDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new BrandDto
        {
            Id = 1,
            Name = "Apple",
            Description = "Apple Inc.",
            IsActive = true
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Apple", dto.Name);
        Assert.Equal("Apple Inc.", dto.Description);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void BrandDto_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var dto = new BrandDto
            {
                Name = null!
            };
        });
    }

    [Fact]
    public void BrandDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new BrandDto
        {
            Name = "Apple",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }

    [Fact]
    public void BrandCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new BrandCreateDto
        {
            Name = "New Brand",
            Description = "New brand description"
        };

        // Assert
        Assert.Equal("New Brand", dto.Name);
        Assert.Equal("New brand description", dto.Description);
    }

    [Fact]
    public void BrandCreateDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new BrandCreateDto
        {
            Name = "New Brand",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }

    [Fact]
    public void BrandUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new BrandUpdateDto
        {
            Name = "Updated Brand",
            Description = "Updated description",
            IsActive = false
        };

        // Assert
        Assert.Equal("Updated Brand", dto.Name);
        Assert.Equal("Updated description", dto.Description);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void BrandUpdateDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new BrandUpdateDto
        {
            Name = "Updated Brand",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }
}
