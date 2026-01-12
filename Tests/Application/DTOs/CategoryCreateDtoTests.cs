using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class CategoryCreateDtoTests
{
    [Fact]
    public void CategoryCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new CategoryCreateDto();
        var expectedValue = "Electronics";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void CategoryCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void CategoryCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new CategoryCreateDto();
        var expectedValue = "Electronic devices and accessories";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void CategoryCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void CategoryCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CategoryCreateDto();

        // Act
        dto.Name = "Electronics";
        dto.Description = "Electronic devices and accessories";

        // Assert
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic devices and accessories", dto.Description);
    }
}
