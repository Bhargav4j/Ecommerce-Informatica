using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.DTOs;

public class BrandDtoTests
{
    [Fact]
    public void BrandDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new BrandDto();

        // Assert
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new BrandDto();

        // Act
        dto.Id = 100;
        dto.Name = "Samsung";
        dto.Description = "Electronics brand";
        dto.IsActive = true;
        dto.CreatedDate = new DateTime(2024, 1, 1);

        // Assert
        Assert.Equal(100, dto.Id);
        Assert.Equal("Samsung", dto.Name);
        Assert.Equal("Electronics brand", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(new DateTime(2024, 1, 1), dto.CreatedDate);
    }

    [Fact]
    public void BrandCreateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new BrandCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void BrandCreateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new BrandCreateDto();

        // Act
        dto.Name = "Samsung";
        dto.Description = "Electronics brand";
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Samsung", dto.Name);
        Assert.Equal("Electronics brand", dto.Description);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void BrandUpdateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new BrandUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void BrandUpdateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new BrandUpdateDto();

        // Act
        dto.Name = "Samsung Updated";
        dto.Description = "Updated description";
        dto.IsActive = true;
        dto.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Samsung Updated", dto.Name);
        Assert.Equal("Updated description", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal("Admin", dto.ModifiedBy);
    }
}
