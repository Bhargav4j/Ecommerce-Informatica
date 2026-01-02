using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class ProductTests
{
    [Fact]
    public void Product_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.NotNull(product);
        Assert.Equal(0, product.Id);
        Assert.Equal(string.Empty, product.Name);
        Assert.Null(product.Description);
        Assert.Equal(0, product.Price);
        Assert.Equal(0, product.Stock);
        Assert.Null(product.ImageUrl);
        Assert.Equal(0, product.CategoryId);
        Assert.Equal(0, product.BrandId);
        Assert.Equal(0, product.SupplierId);
        Assert.True(product.IsActive);
        Assert.Equal(string.Empty, product.CreatedBy);
        Assert.Null(product.ModifiedBy);
        Assert.Null(product.ModifiedDate);
        Assert.NotNull(product.InvoiceDetails);
        Assert.Empty(product.InvoiceDetails);
    }

    [Fact]
    public void Product_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var product = new Product();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);
        var category = new Category { Id = 1, Name = "Electronics" };
        var brand = new Brand { Id = 1, Name = "Apple" };
        var supplier = new Supplier { Id = 1, Name = "Tech Supplies" };

        // Act
        product.Id = 1;
        product.Name = "iPhone 13";
        product.Description = "Latest iPhone model";
        product.Price = 999.99m;
        product.Stock = 50;
        product.ImageUrl = "https://example.com/iphone.jpg";
        product.CategoryId = 1;
        product.Category = category;
        product.BrandId = 1;
        product.Brand = brand;
        product.SupplierId = 1;
        product.Supplier = supplier;
        product.IsActive = false;
        product.CreatedDate = createdDate;
        product.ModifiedDate = modifiedDate;
        product.CreatedBy = "Admin";
        product.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, product.Id);
        Assert.Equal("iPhone 13", product.Name);
        Assert.Equal("Latest iPhone model", product.Description);
        Assert.Equal(999.99m, product.Price);
        Assert.Equal(50, product.Stock);
        Assert.Equal("https://example.com/iphone.jpg", product.ImageUrl);
        Assert.Equal(1, product.CategoryId);
        Assert.Equal(category, product.Category);
        Assert.Equal(1, product.BrandId);
        Assert.Equal(brand, product.Brand);
        Assert.Equal(1, product.SupplierId);
        Assert.Equal(supplier, product.Supplier);
        Assert.False(product.IsActive);
        Assert.Equal(createdDate, product.CreatedDate);
        Assert.Equal(modifiedDate, product.ModifiedDate);
        Assert.Equal("Admin", product.CreatedBy);
        Assert.Equal("System", product.ModifiedBy);
    }

    [Fact]
    public void Product_InvoiceDetails_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.NotNull(product.InvoiceDetails);
        Assert.IsAssignableFrom<ICollection<InvoiceDetail>>(product.InvoiceDetails);
        Assert.Empty(product.InvoiceDetails);
    }

    [Fact]
    public void Product_InvoiceDetails_ShouldAllowAddingInvoiceDetails()
    {
        // Arrange
        var product = new Product { Id = 1 };
        var invoiceDetail = new InvoiceDetail { ProductId = product.Id };

        // Act
        product.InvoiceDetails.Add(invoiceDetail);

        // Assert
        Assert.Single(product.InvoiceDetails);
        Assert.Contains(invoiceDetail, product.InvoiceDetails);
    }

    [Fact]
    public void Product_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.True(product.IsActive);
    }

    [Fact]
    public void Product_Price_ShouldAcceptDecimalValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = 49.99m;

        // Assert
        Assert.Equal(49.99m, product.Price);
    }

    [Fact]
    public void Product_Price_ShouldAcceptZeroValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = 0m;

        // Assert
        Assert.Equal(0m, product.Price);
    }

    [Fact]
    public void Product_Stock_ShouldAcceptPositiveInteger()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = 100;

        // Assert
        Assert.Equal(100, product.Stock);
    }

    [Fact]
    public void Product_Stock_ShouldAcceptZeroValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = 0;

        // Assert
        Assert.Equal(0, product.Stock);
    }

    [Fact]
    public void Product_Description_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Description = null;

        // Assert
        Assert.Null(product.Description);
    }

    [Fact]
    public void Product_ImageUrl_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = null;

        // Assert
        Assert.Null(product.ImageUrl);
    }

    [Fact]
    public void Product_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.Null(product.ModifiedDate);
    }

    [Fact]
    public void Product_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.Null(product.ModifiedBy);
    }
}
