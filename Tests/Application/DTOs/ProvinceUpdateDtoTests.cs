using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class ProvinceUpdateDtoTests
{
    [Fact]
    public void ProvinceUpdateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new ProvinceUpdateDto();
        var expectedValue = "Buenos Aires";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void ProvinceUpdateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ProvinceUpdateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new ProvinceUpdateDto();
        var expectedValue = "Province of Buenos Aires";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void ProvinceUpdateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void ProvinceUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new ProvinceUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void ProvinceUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new ProvinceUpdateDto();

        // Act
        dto.Name = "Buenos Aires";
        dto.Description = "Province of Buenos Aires";
        dto.IsActive = true;

        // Assert
        Assert.Equal("Buenos Aires", dto.Name);
        Assert.Equal("Province of Buenos Aires", dto.Description);
        Assert.True(dto.IsActive);
    }
}
