using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class PaymentMethodUpdateDtoTests
{
    [Fact]
    public void PaymentMethodUpdateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new PaymentMethodUpdateDto();
        var expectedValue = "Credit Card";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void PaymentMethodUpdateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void PaymentMethodUpdateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new PaymentMethodUpdateDto();
        var expectedValue = "Payment via credit card";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void PaymentMethodUpdateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void PaymentMethodUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new PaymentMethodUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void PaymentMethodUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PaymentMethodUpdateDto();

        // Act
        dto.Name = "Credit Card";
        dto.Description = "Payment via credit card";
        dto.IsActive = true;

        // Assert
        Assert.Equal("Credit Card", dto.Name);
        Assert.Equal("Payment via credit card", dto.Description);
        Assert.True(dto.IsActive);
    }
}
