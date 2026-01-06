using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.DTOs.Tests;

/// <summary>
/// Test class for CategoryDto, CategoryCreateDto, and CategoryUpdateDto
/// </summary>
public class CategoryDtoTests
{
    [Fact]
    public void CategoryDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new CategoryDto
        {
            Id = 1,
            Name = "Electronics",
            Description = "Electronic products",
            IsActive = true
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic products", dto.Description);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void CategoryDto_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var dto = new CategoryDto
            {
                Name = null!
            };
        });
    }

    [Fact]
    public void CategoryDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new CategoryDto
        {
            Name = "Electronics",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }

    [Fact]
    public void CategoryCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new CategoryCreateDto
        {
            Name = "New Category",
            Description = "New category description"
        };

        // Assert
        Assert.Equal("New Category", dto.Name);
        Assert.Equal("New category description", dto.Description);
    }

    [Fact]
    public void CategoryCreateDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new CategoryCreateDto
        {
            Name = "New Category",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }

    [Fact]
    public void CategoryUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new CategoryUpdateDto
        {
            Name = "Updated Category",
            Description = "Updated description",
            IsActive = false
        };

        // Assert
        Assert.Equal("Updated Category", dto.Name);
        Assert.Equal("Updated description", dto.Description);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void CategoryUpdateDto_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new CategoryUpdateDto
        {
            Name = "Updated Category",
            Description = null
        };

        // Assert
        Assert.Null(dto.Description);
    }
}
