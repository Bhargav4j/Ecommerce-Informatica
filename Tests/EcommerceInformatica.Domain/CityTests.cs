using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;

namespace EcommerceInformatica.Domain.Tests;

public class CityTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var city = new City();

        // Assert
        Assert.NotNull(city);
        Assert.Equal(0, city.Id);
        Assert.Equal(string.Empty, city.Name);
        Assert.Equal(string.Empty, city.Description);
        Assert.False(city.IsActive);
        Assert.Equal(string.Empty, city.CreatedBy);
        Assert.Null(city.ModifiedBy);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.Id = 30;

        // Assert
        Assert.Equal(30, city.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.Name = "Los Angeles";

        // Assert
        Assert.Equal("Los Angeles", city.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.Description = "City of Angels";

        // Assert
        Assert.Equal("City of Angels", city.Description);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.IsActive = true;

        // Assert
        Assert.True(city.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var city = new City();
        var date = new DateTime(2026, 1, 12);

        // Act
        city.CreatedDate = date;

        // Assert
        Assert.Equal(date, city.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var city = new City();
        var date = new DateTime(2026, 1, 12);

        // Act
        city.ModifiedDate = date;

        // Assert
        Assert.Equal(date, city.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var city = new City();

        // Act
        city.ModifiedDate = null;

        // Assert
        Assert.Null(city.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", city.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", city.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var city = new City();

        // Act
        city.ModifiedBy = null;

        // Assert
        Assert.Null(city.ModifiedBy);
    }

    [Fact]
    public void SetProvinceId_ShouldSetValue()
    {
        // Arrange
        var city = new City();

        // Act
        city.ProvinceId = 5;

        // Assert
        Assert.Equal(5, city.ProvinceId);
    }

    [Fact]
    public void SetProvince_ShouldSetValue()
    {
        // Arrange
        var city = new City();
        var province = new Province { Id = 10, Name = "California" };

        // Act
        city.Province = province;

        // Assert
        Assert.NotNull(city.Province);
        Assert.Equal(10, city.Province.Id);
        Assert.Equal("California", city.Province.Name);
    }
}
