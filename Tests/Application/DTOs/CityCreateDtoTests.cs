using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class CityCreateDtoTests
{
    [Fact]
    public void CityCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new CityCreateDto();
        var expectedValue = "La Plata";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void CityCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void CityCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new CityCreateDto();
        var expectedValue = "Capital city of Buenos Aires Province";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void CityCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void CityCreateDto_ShouldSetAndGetProvinceId()
    {
        // Arrange
        var dto = new CityCreateDto();
        var expectedValue = 1;

        // Act
        dto.ProvinceId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProvinceId);
    }

    [Fact]
    public void CityCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CityCreateDto();

        // Act
        dto.Name = "La Plata";
        dto.Description = "Capital city of Buenos Aires Province";
        dto.ProvinceId = 5;

        // Assert
        Assert.Equal("La Plata", dto.Name);
        Assert.Equal("Capital city of Buenos Aires Province", dto.Description);
        Assert.Equal(5, dto.ProvinceId);
    }
}
