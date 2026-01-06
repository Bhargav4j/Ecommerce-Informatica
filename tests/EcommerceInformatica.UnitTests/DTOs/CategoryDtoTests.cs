using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.DTOs;

public class CategoryDtoTests
{
    [Fact]
    public void CategoryDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new CategoryDto();

        // Assert
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new CategoryDto();

        // Act
        dto.Id = 100;
        dto.Name = "Electronics";
        dto.Description = "Electronic devices";
        dto.IsActive = true;
        dto.CreatedDate = new DateTime(2024, 1, 1);

        // Assert
        Assert.Equal(100, dto.Id);
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic devices", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(new DateTime(2024, 1, 1), dto.CreatedDate);
    }

    [Fact]
    public void CategoryCreateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new CategoryCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void CategoryCreateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new CategoryCreateDto();

        // Act
        dto.Name = "Electronics";
        dto.Description = "Electronic devices";
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic devices", dto.Description);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new CategoryUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new CategoryUpdateDto();

        // Act
        dto.Name = "Electronics Updated";
        dto.Description = "Updated description";
        dto.IsActive = true;
        dto.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Electronics Updated", dto.Name);
        Assert.Equal("Updated description", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal("Admin", dto.ModifiedBy);
    }
}
