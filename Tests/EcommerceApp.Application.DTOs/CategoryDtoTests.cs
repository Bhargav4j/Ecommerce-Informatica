using Xunit;
using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.DTOs.Tests;

public class CategoryDtoTests
{
    [Fact]
    public void CategoryDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new CategoryDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void CategoryDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new CategoryDto();

        // Act
        dto.Id = 1;
        dto.Name = "Electronics";
        dto.Description = "Electronic items";
        dto.IsActive = true;

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic items", dto.Description);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void CategoryCreateDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new CategoryCreateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void CategoryCreateDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new CategoryCreateDto();

        // Act
        dto.Name = "Books";
        dto.Description = "All kinds of books";
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Books", dto.Name);
        Assert.Equal("All kinds of books", dto.Description);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void CategoryUpdateDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new CategoryUpdateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void CategoryUpdateDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new CategoryUpdateDto();

        // Act
        dto.Name = "Updated Category";
        dto.Description = "Updated description";
        dto.IsActive = true;
        dto.ModifiedBy = "System";

        // Assert
        Assert.Equal("Updated Category", dto.Name);
        Assert.Equal("Updated description", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal("System", dto.ModifiedBy);
    }
}
