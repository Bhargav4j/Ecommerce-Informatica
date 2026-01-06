using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class ProductTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var product = new Product();

        // Assert
        Assert.Equal(0, product.Id);
        Assert.Equal(string.Empty, product.Name);
        Assert.Null(product.Description);
        Assert.Equal(0, product.UnitPrice);
        Assert.Equal(0, product.Stock);
        Assert.True(product.IsActive);
        Assert.InRange(product.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(product.ModifiedDate);
        Assert.Equal(string.Empty, product.CreatedBy);
        Assert.Null(product.ModifiedBy);
        Assert.NotNull(product.InvoiceDetails);
        Assert.Empty(product.InvoiceDetails);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = 100;

        // Assert
        Assert.Equal(100, product.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = "Laptop";

        // Assert
        Assert.Equal("Laptop", product.Name);
    }

    [Fact]
    public void UnitPrice_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.UnitPrice = 1500.50m;

        // Assert
        Assert.Equal(1500.50m, product.UnitPrice);
    }

    [Fact]
    public void Stock_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = 50;

        // Assert
        Assert.Equal(50, product.Stock);
    }

    [Fact]
    public void CategoryId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.CategoryId = 5;

        // Assert
        Assert.Equal(5, product.CategoryId);
    }

    [Fact]
    public void BrandId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.BrandId = 10;

        // Assert
        Assert.Equal(10, product.BrandId);
    }

    [Fact]
    public void SupplierId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.SupplierId = 15;

        // Assert
        Assert.Equal(15, product.SupplierId);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.IsActive = false;

        // Assert
        Assert.False(product.IsActive);
    }

    [Fact]
    public void UnitPrice_ShouldHandleZeroValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.UnitPrice = 0;

        // Assert
        Assert.Equal(0, product.UnitPrice);
    }

    [Fact]
    public void UnitPrice_ShouldHandleNegativeValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.UnitPrice = -100;

        // Assert
        Assert.Equal(-100, product.UnitPrice);
    }

    [Fact]
    public void Stock_ShouldHandleZeroValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = 0;

        // Assert
        Assert.Equal(0, product.Stock);
    }

    [Fact]
    public void Stock_ShouldHandleNegativeValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = -10;

        // Assert
        Assert.Equal(-10, product.Stock);
    }

    [Fact]
    public void InvoiceDetails_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var product = new Product();

        // Assert
        Assert.NotNull(product.InvoiceDetails);
        Assert.Empty(product.InvoiceDetails);
        Assert.IsAssignableFrom<ICollection<InvoiceDetail>>(product.InvoiceDetails);
    }

    [Fact]
    public void Product_ShouldAllowInvoiceDetailsToBeAdded()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop" };
        var invoiceDetail = new InvoiceDetail { Id = 1, ProductId = 1 };

        // Act
        product.InvoiceDetails.Add(invoiceDetail);

        // Assert
        Assert.Single(product.InvoiceDetails);
        Assert.Contains(invoiceDetail, product.InvoiceDetails);
    }
}
