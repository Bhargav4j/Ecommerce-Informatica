using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Person entity
/// </summary>
public class PersonTests
{
    [Fact]
    public void Person_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "123-456-7890",
            Address = "123 Main St",
            CityId = 1,
            ProvinceId = 1,
            Username = "johndoe",
            PasswordHash = "hashedpassword",
            Role = "Customer",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "System"
        };

        // Assert
        Assert.Equal(1, person.Id);
        Assert.Equal("John", person.FirstName);
        Assert.Equal("Doe", person.LastName);
        Assert.Equal("john.doe@example.com", person.Email);
        Assert.Equal("123-456-7890", person.Phone);
        Assert.Equal("123 Main St", person.Address);
        Assert.Equal(1, person.CityId);
        Assert.Equal(1, person.ProvinceId);
        Assert.Equal("johndoe", person.Username);
        Assert.Equal("hashedpassword", person.PasswordHash);
        Assert.Equal("Customer", person.Role);
        Assert.True(person.IsActive);
    }

    [Fact]
    public void Person_FirstName_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var person = new Person
            {
                FirstName = null!,
                LastName = "Doe",
                Email = "john@example.com",
                Username = "johndoe",
                PasswordHash = "hash",
                Role = "Customer",
                CreatedBy = "System"
            };
        });
    }

    [Fact]
    public void Person_Orders_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System"
        };

        // Assert
        Assert.NotNull(person.Orders);
        Assert.Empty(person.Orders);
    }

    [Fact]
    public void Person_Phone_ShouldBeNullable()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System",
            Phone = null
        };

        // Assert
        Assert.Null(person.Phone);
    }

    [Fact]
    public void Person_Address_ShouldBeNullable()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System",
            Address = null
        };

        // Assert
        Assert.Null(person.Address);
    }

    [Fact]
    public void Person_CityId_ShouldBeNullable()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System",
            CityId = null
        };

        // Assert
        Assert.Null(person.CityId);
    }

    [Fact]
    public void Person_ProvinceId_ShouldBeNullable()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System",
            ProvinceId = null
        };

        // Assert
        Assert.Null(person.ProvinceId);
    }

    [Fact]
    public void Person_NavigationProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System"
        };

        // Assert
        Assert.Null(person.City);
        Assert.Null(person.Province);
    }

    [Fact]
    public void Person_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System"
        };

        // Assert
        Assert.False(person.IsActive);
    }

    [Fact]
    public void Person_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(person.ModifiedDate);
    }
}
