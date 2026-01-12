using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class CityDtoTests
{
    [Fact]
    public void CityDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = "La Plata";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void CityDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = "Capital city of Buenos Aires Province";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void CityDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void CityDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new CityDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void CityDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new CityDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void CityDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new CityDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetProvinceId()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = 1;

        // Act
        dto.ProvinceId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProvinceId);
    }

    [Fact]
    public void CityDto_ShouldSetAndGetProvinceName()
    {
        // Arrange
        var dto = new CityDto();
        var expectedValue = "Buenos Aires";

        // Act
        dto.ProvinceName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProvinceName);
    }

    [Fact]
    public void CityDto_ProvinceName_ShouldAllowNull()
    {
        // Arrange
        var dto = new CityDto();

        // Act
        dto.ProvinceName = null;

        // Assert
        Assert.Null(dto.ProvinceName);
    }

    [Fact]
    public void CityDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CityDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "La Plata";
        dto.Description = "Capital city of Buenos Aires Province";
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";
        dto.ProvinceId = 5;
        dto.ProvinceName = "Buenos Aires";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("La Plata", dto.Name);
        Assert.Equal("Capital city of Buenos Aires Province", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
        Assert.Equal(5, dto.ProvinceId);
        Assert.Equal("Buenos Aires", dto.ProvinceName);
    }
}
