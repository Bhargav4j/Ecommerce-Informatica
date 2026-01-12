using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class BrandUpdateDtoTests
{
    [Fact]
    public void BrandUpdateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new BrandUpdateDto();
        var expectedValue = "Samsung";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void BrandUpdateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void BrandUpdateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new BrandUpdateDto();
        var expectedValue = "Leading electronics manufacturer";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void BrandUpdateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void BrandUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new BrandUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void BrandUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new BrandUpdateDto();

        // Act
        dto.Name = "Samsung";
        dto.Description = "Leading electronics manufacturer";
        dto.IsActive = true;

        // Assert
        Assert.Equal("Samsung", dto.Name);
        Assert.Equal("Leading electronics manufacturer", dto.Description);
        Assert.True(dto.IsActive);
    }
}
