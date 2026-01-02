using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class ProvinceTests
{
    [Fact]
    public void Province_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.NotNull(province);
        Assert.Equal(0, province.Id);
        Assert.Equal(string.Empty, province.Name);
        Assert.True(province.IsActive);
        Assert.Equal(string.Empty, province.CreatedBy);
        Assert.Null(province.ModifiedBy);
        Assert.Null(province.ModifiedDate);
        Assert.NotNull(province.Cities);
        Assert.Empty(province.Cities);
    }

    [Fact]
    public void Province_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var province = new Province();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);

        // Act
        province.Id = 1;
        province.Name = "Buenos Aires";
        province.IsActive = false;
        province.CreatedDate = createdDate;
        province.ModifiedDate = modifiedDate;
        province.CreatedBy = "Admin";
        province.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, province.Id);
        Assert.Equal("Buenos Aires", province.Name);
        Assert.False(province.IsActive);
        Assert.Equal(createdDate, province.CreatedDate);
        Assert.Equal(modifiedDate, province.ModifiedDate);
        Assert.Equal("Admin", province.CreatedBy);
        Assert.Equal("System", province.ModifiedBy);
    }

    [Fact]
    public void Province_Cities_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.NotNull(province.Cities);
        Assert.IsAssignableFrom<ICollection<City>>(province.Cities);
        Assert.Empty(province.Cities);
    }

    [Fact]
    public void Province_Cities_ShouldAllowAddingCities()
    {
        // Arrange
        var province = new Province { Id = 1 };
        var city = new City { Id = 1, ProvinceId = province.Id };

        // Act
        province.Cities.Add(city);

        // Assert
        Assert.Single(province.Cities);
        Assert.Contains(city, province.Cities);
    }

    [Fact]
    public void Province_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.True(province.IsActive);
    }

    [Fact]
    public void Province_Name_ShouldAcceptStringValue()
    {
        // Arrange
        var province = new Province();
        var name = "Cordoba";

        // Act
        province.Name = name;

        // Assert
        Assert.Equal(name, province.Name);
    }

    [Fact]
    public void Province_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.Null(province.ModifiedDate);
    }

    [Fact]
    public void Province_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var province = new Province();

        // Assert
        Assert.Null(province.ModifiedBy);
    }
}
