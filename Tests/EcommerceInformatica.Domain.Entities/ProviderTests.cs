using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Provider entity
/// </summary>
public class ProviderTests
{
    [Fact]
    public void Provider_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Id = 1,
            Name = "Tech Supplier",
            Email = "contact@techsupplier.com",
            Phone = "123-456-7890",
            Address = "123 Tech Street",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, provider.Id);
        Assert.Equal("Tech Supplier", provider.Name);
        Assert.Equal("contact@techsupplier.com", provider.Email);
        Assert.Equal("123-456-7890", provider.Phone);
        Assert.Equal("123 Tech Street", provider.Address);
        Assert.True(provider.IsActive);
        Assert.NotNull(provider.CreatedBy);
    }

    [Fact]
    public void Provider_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var provider = new Provider
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void Provider_Products_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(provider.Products);
        Assert.Empty(provider.Products);
    }

    [Fact]
    public void Provider_Email_ShouldBeNullable()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser",
            Email = null
        };

        // Assert
        Assert.Null(provider.Email);
    }

    [Fact]
    public void Provider_Phone_ShouldBeNullable()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser",
            Phone = null
        };

        // Assert
        Assert.Null(provider.Phone);
    }

    [Fact]
    public void Provider_Address_ShouldBeNullable()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser",
            Address = null
        };

        // Assert
        Assert.Null(provider.Address);
    }

    [Fact]
    public void Provider_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(provider.ModifiedDate);
    }

    [Fact]
    public void Provider_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(provider.IsActive);
    }

    [Fact]
    public void Provider_WithProducts_ShouldMaintainRelationship()
    {
        // Arrange
        var provider = new Provider
        {
            Name = "Tech Supplier",
            CreatedBy = "TestUser"
        };

        var product = new Product
        {
            Name = "Laptop",
            CreatedBy = "TestUser",
            ProviderId = provider.Id
        };

        // Act
        provider.Products.Add(product);

        // Assert
        Assert.Single(provider.Products);
        Assert.Equal(product, provider.Products.First());
    }
}
