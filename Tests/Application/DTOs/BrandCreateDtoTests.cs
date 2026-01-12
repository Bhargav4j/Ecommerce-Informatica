using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class BrandCreateDtoTests
{
    [Fact]
    public void BrandCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new BrandCreateDto();
        var expectedValue = "Samsung";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void BrandCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void BrandCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new BrandCreateDto();
        var expectedValue = "Leading electronics manufacturer";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void BrandCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void BrandCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new BrandCreateDto();

        // Act
        dto.Name = "Samsung";
        dto.Description = "Leading electronics manufacturer";

        // Assert
        Assert.Equal("Samsung", dto.Name);
        Assert.Equal("Leading electronics manufacturer", dto.Description);
    }
}
