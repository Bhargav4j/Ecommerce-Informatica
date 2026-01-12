using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class SupplierDtoTests
{
    [Fact]
    public void SupplierDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetName()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "Tech Suppliers Inc";

        // Act
        dto.Name = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Name);
    }

    [Fact]
    public void SupplierDto_ShouldInitializeNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetDescription()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "Leading technology supplier";

        // Act
        dto.Description = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Description);
    }

    [Fact]
    public void SupplierDto_ShouldInitializeDescriptionWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(string.Empty, dto.Description);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetContactName()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "John Doe";

        // Act
        dto.ContactName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ContactName);
    }

    [Fact]
    public void SupplierDto_ShouldInitializeContactNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(string.Empty, dto.ContactName);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetContactPhone()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "+1234567890";

        // Act
        dto.ContactPhone = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ContactPhone);
    }

    [Fact]
    public void SupplierDto_ShouldInitializeContactPhoneWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(string.Empty, dto.ContactPhone);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetContactEmail()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "contact@techsuppliers.com";

        // Act
        dto.ContactEmail = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ContactEmail);
    }

    [Fact]
    public void SupplierDto_ShouldInitializeContactEmailWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(string.Empty, dto.ContactEmail);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void SupplierDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new SupplierDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void SupplierDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new SupplierDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void SupplierDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new SupplierDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void SupplierDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new SupplierDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Name = "Tech Suppliers Inc";
        dto.Description = "Leading technology supplier";
        dto.ContactName = "John Doe";
        dto.ContactPhone = "+1234567890";
        dto.ContactEmail = "contact@techsuppliers.com";
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Tech Suppliers Inc", dto.Name);
        Assert.Equal("Leading technology supplier", dto.Description);
        Assert.Equal("John Doe", dto.ContactName);
        Assert.Equal("+1234567890", dto.ContactPhone);
        Assert.Equal("contact@techsuppliers.com", dto.ContactEmail);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
    }
}
