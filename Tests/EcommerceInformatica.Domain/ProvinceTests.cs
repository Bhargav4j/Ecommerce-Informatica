using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class ProvinceTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.NotNull(province);
        Assert.Equal(0, province.Id);
        Assert.Equal(string.Empty, province.Name);
        Assert.Equal(string.Empty, province.Description);
        Assert.False(province.IsActive);
        Assert.Equal(string.Empty, province.CreatedBy);
        Assert.Null(province.ModifiedBy);
        Assert.NotNull(province.Cities);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var province = new Province();

        // Act
        province.Id = 20;

        // Assert
        Assert.Equal(20, province.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var province = new Province();

        // Act
        province.Name = "California";

        // Assert
        Assert.Equal("California", province.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var province = new Province();

        // Act
        province.Description = "State of California";

        // Assert
        Assert.Equal("State of California", province.Description);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var province = new Province();

        // Act
        province.IsActive = true;

        // Assert
        Assert.True(province.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var province = new Province();
        var date = new DateTime(2026, 1, 12);

        // Act
        province.CreatedDate = date;

        // Assert
        Assert.Equal(date, province.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var province = new Province();
        var date = new DateTime(2026, 1, 12);

        // Act
        province.ModifiedDate = date;

        // Assert
        Assert.Equal(date, province.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var province = new Province();

        // Act
        province.ModifiedDate = null;

        // Assert
        Assert.Null(province.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var province = new Province();

        // Act
        province.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", province.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var province = new Province();

        // Act
        province.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", province.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var province = new Province();

        // Act
        province.ModifiedBy = null;

        // Assert
        Assert.Null(province.ModifiedBy);
    }

    [Fact]
    public void SetCities_ShouldSetCollection()
    {
        // Arrange
        var province = new Province();
        var cities = new List<City>
        {
            new City { Id = 1, Name = "Los Angeles" },
            new City { Id = 2, Name = "San Francisco" }
        };

        // Act
        province.Cities = cities;

        // Assert
        Assert.NotNull(province.Cities);
        Assert.Equal(2, province.Cities.Count);
    }

    [Fact]
    public void Cities_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.NotNull(province.Cities);
        Assert.Empty(province.Cities);
    }
}
