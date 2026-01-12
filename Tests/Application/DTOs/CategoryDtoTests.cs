using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class CategoryDtoTests
{
    [Fact]
    public void CategoryDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = "Electronics";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void CategoryDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = "Electronic devices and accessories";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void CategoryDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void CategoryDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new CategoryDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void CategoryDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new CategoryDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void CategoryDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new CategoryDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void CategoryDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new CategoryDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void CategoryDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CategoryDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "Electronics";
        dto.Description = "Electronic devices and accessories";
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Electronic devices and accessories", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
    }
}
