using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Province entity
/// </summary>
public class ProvinceTests
{
    [Fact]
    public void Province_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var province = new Province
        {
            Id = 1,
            Name = "Buenos Aires",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, province.Id);
        Assert.Equal("Buenos Aires", province.Name);
        Assert.True(province.IsActive);
        Assert.NotNull(province.CreatedBy);
    }

    [Fact]
    public void Province_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var province = new Province
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void Province_Cities_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var province = new Province
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(province.Cities);
        Assert.Empty(province.Cities);
    }

    [Fact]
    public void Province_Persons_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var province = new Province
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(province.Persons);
        Assert.Empty(province.Persons);
    }

    [Fact]
    public void Province_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var province = new Province
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(province.ModifiedDate);
    }

    [Fact]
    public void Province_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var province = new Province
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(province.IsActive);
    }

    [Fact]
    public void Province_CreatedBy_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var province = new Province
            {
                Name = "Buenos Aires",
                CreatedBy = null!
            };
        });
    }

    [Fact]
    public void Province_ModifiedBy_ShouldBeNullable()
    {
        // Arrange & Act
        var province = new Province
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser",
            ModifiedBy = null
        };

        // Assert
        Assert.Null(province.ModifiedBy);
    }

    [Fact]
    public void Province_WithCities_ShouldMaintainRelationship()
    {
        // Arrange
        var province = new Province
        {
            Name = "Buenos Aires",
            CreatedBy = "TestUser"
        };

        var city = new City
        {
            Name = "La Plata",
            CreatedBy = "TestUser",
            ProvinceId = province.Id
        };

        // Act
        province.Cities.Add(city);

        // Assert
        Assert.Single(province.Cities);
        Assert.Equal(city, province.Cities.First());
    }
}
