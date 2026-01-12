using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class SupplierCreateDtoTests
{
    [Fact]
    public void SupplierCreateDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new SupplierCreateDto();
        var expectedValue = "Tech Suppliers Inc";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void SupplierCreateDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void SupplierCreateDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new SupplierCreateDto();
        var expectedValue = "Leading technology supplier";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void SupplierCreateDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void SupplierCreateDto_ShouldSetAndGetContactName()
    {
        // Arrange
        var dto = new SupplierCreateDto();
        var expectedValue = "John Doe";

        // Act
        dto.ContactName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ContactName);
    }

    [Fact]
    public void SupplierCreateDto_ShouldInitializeContactNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.ContactName);
    }

    [Fact]
    public void SupplierCreateDto_ShouldSetAndGetContactPhone()
    {
        // Arrange
        var dto = new SupplierCreateDto();
        var expectedValue = "+1234567890";

        // Act
        dto.ContactPhone = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ContactPhone);
    }

    [Fact]
    public void SupplierCreateDto_ShouldInitializeContactPhoneWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.ContactPhone);
    }

    [Fact]
    public void SupplierCreateDto_ShouldSetAndGetContactEmail()
    {
        // Arrange
        var dto = new SupplierCreateDto();
        var expectedValue = "contact@techsuppliers.com";

        // Act
        dto.ContactEmail = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ContactEmail);
    }

    [Fact]
    public void SupplierCreateDto_ShouldInitializeContactEmailWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.ContactEmail);
    }

    [Fact]
    public void SupplierCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new SupplierCreateDto();

        // Act
        dto.Name = "Tech Suppliers Inc";
        dto.Description = "Leading technology supplier";
        dto.ContactName = "John Doe";
        dto.ContactPhone = "+1234567890";
        dto.ContactEmail = "contact@techsuppliers.com";

        // Assert
        Assert.Equal("Tech Suppliers Inc", dto.Name);
        Assert.Equal("Leading technology supplier", dto.Description);
        Assert.Equal("John Doe", dto.ContactName);
        Assert.Equal("+1234567890", dto.ContactPhone);
        Assert.Equal("contact@techsuppliers.com", dto.ContactEmail);
    }
}
