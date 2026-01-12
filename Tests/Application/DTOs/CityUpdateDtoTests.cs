using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class CityUpdateDtoTests
{
    [Fact]
    public void CityUpdateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new CityUpdateDto();
        var expectedValue = "La Plata";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void CityUpdateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void CityUpdateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new CityUpdateDto();
        var expectedValue = "Capital city of Buenos Aires Province";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void CityUpdateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void CityUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new CityUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void CityUpdateDto_ShouldSetAndGetProvinceId()
    {
        // Arrange
        var dto = new CityUpdateDto();
        var expectedValue = 1;

        // Act
        dto.ProvinceId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProvinceId);
    }

    [Fact]
    public void CityUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CityUpdateDto();

        // Act
        dto.Name = "La Plata";
        dto.Description = "Capital city of Buenos Aires Province";
        dto.IsActive = true;
        dto.ProvinceId = 5;

        // Assert
        Assert.Equal("La Plata", dto.Name);
        Assert.Equal("Capital city of Buenos Aires Province", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(5, dto.ProvinceId);
    }
}
