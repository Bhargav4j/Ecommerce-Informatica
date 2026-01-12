using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class ProvinceDtoTests
{
    [Fact]
    public void ProvinceDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = "Buenos Aires";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void ProvinceDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = "Province of Buenos Aires";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void ProvinceDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void ProvinceDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProvinceDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void ProvinceDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new ProvinceDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new ProvinceDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void ProvinceDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new ProvinceDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void ProvinceDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new ProvinceDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "Buenos Aires";
        dto.Description = "Province of Buenos Aires";
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Buenos Aires", dto.Name);
        Assert.Equal("Province of Buenos Aires", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
    }
}
