using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class InvoiceDetailDtoTests
{
    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetQuantity()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = 5;

        // Act
        dto.Quantity = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Quantity);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetUnitPrice()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = 99.99m;

        // Act
        dto.UnitPrice = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.UnitPrice);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetSubtotal()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = 499.95m;

        // Act
        dto.Subtotal = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Subtotal);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void InvoiceDetailDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDetailDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new InvoiceDetailDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void InvoiceDetailDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDetailDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetInvoiceId()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = 1;

        // Act
        dto.InvoiceId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.InvoiceId);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetProductId()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = 1;

        // Act
        dto.ProductId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProductId);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAndGetProductName()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var expectedValue = "Laptop";

        // Act
        dto.ProductName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProductName);
    }

    [Fact]
    public void InvoiceDetailDto_ProductName_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDetailDto();

        // Act
        dto.ProductName = null;

        // Assert
        Assert.Null(dto.ProductName);
    }

    [Fact]
    public void InvoiceDetailDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new InvoiceDetailDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Quantity = 5;
        dto.UnitPrice = 99.99m;
        dto.Subtotal = 499.95m;
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";
        dto.InvoiceId = 10;
        dto.ProductId = 20;
        dto.ProductName = "Laptop";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal(5, dto.Quantity);
        Assert.Equal(99.99m, dto.UnitPrice);
        Assert.Equal(499.95m, dto.Subtotal);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
        Assert.Equal(10, dto.InvoiceId);
        Assert.Equal(20, dto.ProductId);
        Assert.Equal("Laptop", dto.ProductName);
    }
}
