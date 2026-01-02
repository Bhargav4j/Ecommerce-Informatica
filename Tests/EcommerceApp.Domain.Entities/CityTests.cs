using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class CityTests
{
    [Fact]
    public void City_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var city = new City();

        // Assert
        Assert.NotNull(city);
        Assert.Equal(0, city.Id);
        Assert.Equal(string.Empty, city.Name);
        Assert.Equal(0, city.ProvinceId);
        Assert.True(city.IsActive);
        Assert.Equal(string.Empty, city.CreatedBy);
        Assert.Null(city.ModifiedBy);
        Assert.Null(city.ModifiedDate);
        Assert.NotNull(city.Suppliers);
        Assert.Empty(city.Suppliers);
    }

    [Fact]
    public void City_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var city = new City();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);
        var province = new Province { Id = 1, Name = "Buenos Aires" };

        // Act
        city.Id = 1;
        city.Name = "La Plata";
        city.ProvinceId = 1;
        city.Province = province;
        city.IsActive = false;
        city.CreatedDate = createdDate;
        city.ModifiedDate = modifiedDate;
        city.CreatedBy = "Admin";
        city.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, city.Id);
        Assert.Equal("La Plata", city.Name);
        Assert.Equal(1, city.ProvinceId);
        Assert.Equal(province, city.Province);
        Assert.False(city.IsActive);
        Assert.Equal(createdDate, city.CreatedDate);
        Assert.Equal(modifiedDate, city.ModifiedDate);
        Assert.Equal("Admin", city.CreatedBy);
        Assert.Equal("System", city.ModifiedBy);
    }

    [Fact]
    public void City_Suppliers_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var city = new City();

        // Assert
        Assert.NotNull(city.Suppliers);
        Assert.IsAssignableFrom<ICollection<Supplier>>(city.Suppliers);
        Assert.Empty(city.Suppliers);
    }

    [Fact]
    public void City_Suppliers_ShouldAllowAddingSuppliers()
    {
        // Arrange
        var city = new City { Id = 1 };
        var supplier = new Supplier { Id = 1, CityId = city.Id };

        // Act
        city.Suppliers.Add(supplier);

        // Assert
        Assert.Single(city.Suppliers);
        Assert.Contains(supplier, city.Suppliers);
    }

    [Fact]
    public void City_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var city = new City();

        // Assert
        Assert.True(city.IsActive);
    }

    [Fact]
    public void City_ProvinceId_ShouldAcceptValidInteger()
    {
        // Arrange
        var city = new City();

        // Act
        city.ProvinceId = 5;

        // Assert
        Assert.Equal(5, city.ProvinceId);
    }

    [Fact]
    public void City_Province_ShouldAcceptProvinceEntity()
    {
        // Arrange
        var city = new City();
        var province = new Province { Id = 1, Name = "Test Province" };

        // Act
        city.Province = province;

        // Assert
        Assert.Equal(province, city.Province);
    }

    [Fact]
    public void City_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var city = new City();

        // Assert
        Assert.Null(city.ModifiedDate);
    }

    [Fact]
    public void City_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var city = new City();

        // Assert
        Assert.Null(city.ModifiedBy);
    }
}
