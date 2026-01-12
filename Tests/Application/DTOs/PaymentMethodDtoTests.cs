using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class PaymentMethodDtoTests
{
    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = "Credit Card";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void PaymentMethodDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = "Payment via credit card";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void PaymentMethodDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void PaymentMethodDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new PaymentMethodDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void PaymentMethodDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void PaymentMethodDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new PaymentMethodDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void PaymentMethodDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PaymentMethodDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "Credit Card";
        dto.Description = "Payment via credit card";
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Credit Card", dto.Name);
        Assert.Equal("Payment via credit card", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
    }
}
