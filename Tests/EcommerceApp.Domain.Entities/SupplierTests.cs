using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class SupplierTests
{
    [Fact]
    public void Supplier_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.NotNull(supplier);
        Assert.Equal(0, supplier.Id);
        Assert.Equal(string.Empty, supplier.Name);
        Assert.Equal(string.Empty, supplier.Address);
        Assert.Equal(string.Empty, supplier.Phone);
        Assert.Equal(string.Empty, supplier.Email);
        Assert.Equal(string.Empty, supplier.ContactPerson);
        Assert.Equal(0, supplier.CityId);
        Assert.True(supplier.IsActive);
        Assert.Equal(string.Empty, supplier.CreatedBy);
        Assert.Null(supplier.ModifiedBy);
        Assert.Null(supplier.ModifiedDate);
        Assert.NotNull(supplier.Products);
        Assert.Empty(supplier.Products);
    }

    [Fact]
    public void Supplier_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var supplier = new Supplier();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);
        var city = new City { Id = 1, Name = "Test City" };

        // Act
        supplier.Id = 1;
        supplier.Name = "Tech Supplies Inc.";
        supplier.Address = "123 Main St";
        supplier.Phone = "555-1234";
        supplier.Email = "contact@techsupplies.com";
        supplier.ContactPerson = "John Doe";
        supplier.CityId = 1;
        supplier.City = city;
        supplier.IsActive = false;
        supplier.CreatedDate = createdDate;
        supplier.ModifiedDate = modifiedDate;
        supplier.CreatedBy = "Admin";
        supplier.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, supplier.Id);
        Assert.Equal("Tech Supplies Inc.", supplier.Name);
        Assert.Equal("123 Main St", supplier.Address);
        Assert.Equal("555-1234", supplier.Phone);
        Assert.Equal("contact@techsupplies.com", supplier.Email);
        Assert.Equal("John Doe", supplier.ContactPerson);
        Assert.Equal(1, supplier.CityId);
        Assert.Equal(city, supplier.City);
        Assert.False(supplier.IsActive);
        Assert.Equal(createdDate, supplier.CreatedDate);
        Assert.Equal(modifiedDate, supplier.ModifiedDate);
        Assert.Equal("Admin", supplier.CreatedBy);
        Assert.Equal("System", supplier.ModifiedBy);
    }

    [Fact]
    public void Supplier_Products_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.NotNull(supplier.Products);
        Assert.IsAssignableFrom<ICollection<Product>>(supplier.Products);
        Assert.Empty(supplier.Products);
    }

    [Fact]
    public void Supplier_Products_ShouldAllowAddingProducts()
    {
        // Arrange
        var supplier = new Supplier { Id = 1 };
        var product = new Product { Id = 1, SupplierId = supplier.Id };

        // Act
        supplier.Products.Add(product);

        // Assert
        Assert.Single(supplier.Products);
        Assert.Contains(product, supplier.Products);
    }

    [Fact]
    public void Supplier_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.True(supplier.IsActive);
    }

    [Fact]
    public void Supplier_Email_ShouldAcceptValidEmailFormat()
    {
        // Arrange
        var supplier = new Supplier();
        var validEmail = "supplier@example.com";

        // Act
        supplier.Email = validEmail;

        // Assert
        Assert.Equal(validEmail, supplier.Email);
    }

    [Fact]
    public void Supplier_City_ShouldAcceptCityEntity()
    {
        // Arrange
        var supplier = new Supplier();
        var city = new City { Id = 1, Name = "Test City" };

        // Act
        supplier.City = city;

        // Assert
        Assert.Equal(city, supplier.City);
    }

    [Fact]
    public void Supplier_CityId_ShouldAcceptValidInteger()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.CityId = 5;

        // Assert
        Assert.Equal(5, supplier.CityId);
    }

    [Fact]
    public void Supplier_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.Null(supplier.ModifiedDate);
    }

    [Fact]
    public void Supplier_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.Null(supplier.ModifiedBy);
    }
}
