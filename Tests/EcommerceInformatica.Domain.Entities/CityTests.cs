using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for City entity
/// </summary>
public class CityTests
{
    [Fact]
    public void City_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var city = new City
        {
            Id = 1,
            Name = "Buenos Aires",
            ProvinceId = 1,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, city.Id);
        Assert.Equal("Buenos Aires", city.Name);
        Assert.Equal(1, city.ProvinceId);
        Assert.True(city.IsActive);
        Assert.NotNull(city.CreatedBy);
    }

    [Fact]
    public void City_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var city = new City
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void City_Persons_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var city = new City
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(city.Persons);
        Assert.Empty(city.Persons);
    }

    [Fact]
    public void City_Province_ShouldBeNullable()
    {
        // Arrange & Act
        var city = new City
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser",
            Province = null
        };

        // Assert
        Assert.Null(city.Province);
    }

    [Fact]
    public void City_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var city = new City
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(city.ModifiedDate);
    }

    [Fact]
    public void City_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var city = new City
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(city.IsActive);
    }

    [Fact]
    public void City_CreatedBy_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var city = new City
            {
                Name = "Buenos Aires",
                CreatedBy = null!
            };
        });
    }

    [Fact]
    public void City_WithPersons_ShouldMaintainRelationship()
    {
        // Arrange
        var city = new City
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            CreatedBy = "System",
            CityId = city.Id
        };

        // Act
        city.Persons.Add(person);

        // Assert
        Assert.Single(city.Persons);
        Assert.Equal(person, city.Persons.First());
    }
}
