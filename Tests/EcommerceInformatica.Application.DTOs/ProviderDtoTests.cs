using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.DTOs.Tests;

/// <summary>
/// Test class for ProviderDto, ProviderCreateDto, and ProviderUpdateDto
/// </summary>
public class ProviderDtoTests
{
    [Fact]
    public void ProviderDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new ProviderDto
        {
            Id = 1,
            Name = "Tech Supplier",
            Email = "contact@techsupplier.com",
            Phone = "123-456-7890",
            Address = "123 Tech Street",
            IsActive = true
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Tech Supplier", dto.Name);
        Assert.Equal("contact@techsupplier.com", dto.Email);
        Assert.Equal("123-456-7890", dto.Phone);
        Assert.Equal("123 Tech Street", dto.Address);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void ProviderDto_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var dto = new ProviderDto
            {
                Name = null!
            };
        });
    }

    [Fact]
    public void ProviderDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new ProviderDto
        {
            Name = "Tech Supplier",
            Email = null,
            Phone = null,
            Address = null
        };

        // Assert
        Assert.Null(dto.Email);
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
    }

    [Fact]
    public void ProviderCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new ProviderCreateDto
        {
            Name = "New Provider",
            Email = "new@provider.com",
            Phone = "555-1234",
            Address = "456 New Street"
        };

        // Assert
        Assert.Equal("New Provider", dto.Name);
        Assert.Equal("new@provider.com", dto.Email);
        Assert.Equal("555-1234", dto.Phone);
        Assert.Equal("456 New Street", dto.Address);
    }

    [Fact]
    public void ProviderCreateDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new ProviderCreateDto
        {
            Name = "New Provider",
            Email = null,
            Phone = null,
            Address = null
        };

        // Assert
        Assert.Null(dto.Email);
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
    }

    [Fact]
    public void ProviderUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new ProviderUpdateDto
        {
            Name = "Updated Provider",
            Email = "updated@provider.com",
            Phone = "555-5678",
            Address = "789 Updated Street",
            IsActive = false
        };

        // Assert
        Assert.Equal("Updated Provider", dto.Name);
        Assert.Equal("updated@provider.com", dto.Email);
        Assert.Equal("555-5678", dto.Phone);
        Assert.Equal("789 Updated Street", dto.Address);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void ProviderUpdateDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new ProviderUpdateDto
        {
            Name = "Updated Provider",
            Email = null,
            Phone = null,
            Address = null
        };

        // Assert
        Assert.Null(dto.Email);
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
    }
}
