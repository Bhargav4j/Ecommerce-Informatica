using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class CategoryUpdateDtoTests
{
    [Fact]
    public void CategoryUpdateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new CategoryUpdateDto();
        var expectedValue = "Electronics";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new CategoryUpdateDto();
        var expectedValue = "Electronic devices and accessories";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new CategoryUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void CategoryUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CategoryUpdateDto();

        // Act
        dto.Name = "Electronics";
        dto.Description = "Electronic devices and accessories";
        dto.IsActive = true;

        // Assert
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic devices and accessories", dto.Description);
        Assert.True(dto.IsActive);
    }
}
