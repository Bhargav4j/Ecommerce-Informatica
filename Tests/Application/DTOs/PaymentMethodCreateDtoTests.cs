using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class PaymentMethodCreateDtoTests
{
    [Fact]
    public void PaymentMethodCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new PaymentMethodCreateDto();
        var expectedValue = "Credit Card";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void PaymentMethodCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void PaymentMethodCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new PaymentMethodCreateDto();
        var expectedValue = "Payment via credit card";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void PaymentMethodCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new PaymentMethodCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void PaymentMethodCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PaymentMethodCreateDto();

        // Act
        dto.Name = "Credit Card";
        dto.Description = "Payment via credit card";

        // Assert
        Assert.Equal("Credit Card", dto.Name);
        Assert.Equal("Payment via credit card", dto.Description);
    }
}
