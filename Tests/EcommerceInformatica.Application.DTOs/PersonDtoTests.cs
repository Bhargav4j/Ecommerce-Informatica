using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.DTOs.Tests;

/// <summary>
/// Test class for PersonDto, PersonCreateDto, and PersonUpdateDto
/// </summary>
public class PersonDtoTests
{
    [Fact]
    public void PersonDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new PersonDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "123-456-7890",
            Address = "123 Main St",
            CityId = 1,
            CityName = "Buenos Aires",
            ProvinceId = 1,
            ProvinceName = "Buenos Aires Province",
            Username = "johndoe",
            Role = "Customer",
            IsActive = true
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Doe", dto.LastName);
        Assert.Equal("john.doe@example.com", dto.Email);
        Assert.Equal("123-456-7890", dto.Phone);
        Assert.Equal("123 Main St", dto.Address);
        Assert.Equal(1, dto.CityId);
        Assert.Equal("Buenos Aires", dto.CityName);
        Assert.Equal(1, dto.ProvinceId);
        Assert.Equal("Buenos Aires Province", dto.ProvinceName);
        Assert.Equal("johndoe", dto.Username);
        Assert.Equal("Customer", dto.Role);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void PersonDto_RequiredProperties_ShouldThrowWhenNull()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var dto = new PersonDto
            {
                FirstName = null!,
                LastName = "Doe",
                Email = "john@example.com",
                Username = "johndoe",
                Role = "Customer"
            };
        });
    }

    [Fact]
    public void PersonDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new PersonDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            Role = "Customer",
            Phone = null,
            Address = null,
            CityId = null,
            CityName = null,
            ProvinceId = null,
            ProvinceName = null
        };

        // Assert
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
        Assert.Null(dto.CityId);
        Assert.Null(dto.CityName);
        Assert.Null(dto.ProvinceId);
        Assert.Null(dto.ProvinceName);
    }

    [Fact]
    public void PersonCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new PersonCreateDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Phone = "555-1234",
            Address = "456 Oak Ave",
            CityId = 2,
            ProvinceId = 2,
            Username = "janesmith",
            Password = "SecurePassword123",
            Role = "Admin"
        };

        // Assert
        Assert.Equal("Jane", dto.FirstName);
        Assert.Equal("Smith", dto.LastName);
        Assert.Equal("jane.smith@example.com", dto.Email);
        Assert.Equal("555-1234", dto.Phone);
        Assert.Equal("456 Oak Ave", dto.Address);
        Assert.Equal(2, dto.CityId);
        Assert.Equal(2, dto.ProvinceId);
        Assert.Equal("janesmith", dto.Username);
        Assert.Equal("SecurePassword123", dto.Password);
        Assert.Equal("Admin", dto.Role);
    }

    [Fact]
    public void PersonCreateDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new PersonCreateDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane@example.com",
            Username = "janesmith",
            Password = "password",
            Role = "Admin",
            Phone = null,
            Address = null,
            CityId = null,
            ProvinceId = null
        };

        // Assert
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
        Assert.Null(dto.CityId);
        Assert.Null(dto.ProvinceId);
    }

    [Fact]
    public void PersonUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto
        {
            FirstName = "John",
            LastName = "Updated",
            Email = "john.updated@example.com",
            Phone = "555-9999",
            Address = "789 Updated St",
            CityId = 3,
            ProvinceId = 3,
            IsActive = false
        };

        // Assert
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Updated", dto.LastName);
        Assert.Equal("john.updated@example.com", dto.Email);
        Assert.Equal("555-9999", dto.Phone);
        Assert.Equal("789 Updated St", dto.Address);
        Assert.Equal(3, dto.CityId);
        Assert.Equal(3, dto.ProvinceId);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void PersonUpdateDto_OptionalProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto
        {
            FirstName = "John",
            LastName = "Updated",
            Email = "john@example.com",
            Phone = null,
            Address = null,
            CityId = null,
            ProvinceId = null
        };

        // Assert
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
        Assert.Null(dto.CityId);
        Assert.Null(dto.ProvinceId);
    }
}
