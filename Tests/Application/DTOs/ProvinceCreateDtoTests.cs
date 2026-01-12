using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class ProvinceCreateDtoTests
{
    [Fact]
    public void ProvinceCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new ProvinceCreateDto();
        var expectedValue = "Buenos Aires";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void ProvinceCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ProvinceCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new ProvinceCreateDto();
        var expectedValue = "Province of Buenos Aires";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void ProvinceCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void ProvinceCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new ProvinceCreateDto();

        // Act
        dto.Name = "Buenos Aires";
        dto.Description = "Province of Buenos Aires";

        // Assert
        Assert.Equal("Buenos Aires", dto.Name);
        Assert.Equal("Province of Buenos Aires", dto.Description);
    }
}
