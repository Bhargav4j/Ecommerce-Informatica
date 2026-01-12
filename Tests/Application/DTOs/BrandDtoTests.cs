using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class BrandDtoTests
{
    [Fact]
    public void BrandDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = "Samsung";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void BrandDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = "Leading electronics manufacturer";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void BrandDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void BrandDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new BrandDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void BrandDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new BrandDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void BrandDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new BrandDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void BrandDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new BrandDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void BrandDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new BrandDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "Samsung";
        dto.Description = "Leading electronics manufacturer";
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Samsung", dto.Name);
        Assert.Equal("Leading electronics manufacturer", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
    }
}
