using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class ProvinceTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var province = new Province();

        // Assert
        Assert.Equal(0, province.Id);
        Assert.Equal(string.Empty, province.Name);
        Assert.True(province.IsActive);
        Assert.InRange(province.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(province.ModifiedDate);
        Assert.Equal("System", province.CreatedBy);
        Assert.Null(province.ModifiedBy);
        Assert.NotNull(province.Cities);
        Assert.Empty(province.Cities);
        Assert.NotNull(province.Persons);
        Assert.Empty(province.Persons);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();

        // Act
        province.Id = 100;

        // Assert
        Assert.Equal(100, province.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();

        // Act
        province.Name = "Buenos Aires";

        // Assert
        Assert.Equal("Buenos Aires", province.Name);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();

        // Act
        province.IsActive = false;

        // Assert
        Assert.False(province.IsActive);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();
        var date = new DateTime(2024, 1, 1);

        // Act
        province.CreatedDate = date;

        // Assert
        Assert.Equal(date, province.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();
        var date = new DateTime(2024, 1, 2);

        // Act
        province.ModifiedDate = date;

        // Assert
        Assert.Equal(date, province.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();

        // Act
        province.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", province.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var province = new Province();

        // Act
        province.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", province.ModifiedBy);
    }

    [Fact]
    public void Cities_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var province = new Province();

        // Assert
        Assert.NotNull(province.Cities);
        Assert.Empty(province.Cities);
        Assert.IsAssignableFrom<ICollection<City>>(province.Cities);
    }

    [Fact]
    public void Persons_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var province = new Province();

        // Assert
        Assert.NotNull(province.Persons);
        Assert.Empty(province.Persons);
        Assert.IsAssignableFrom<ICollection<Person>>(province.Persons);
    }

    [Fact]
    public void Province_ShouldAllowCitiesToBeAdded()
    {
        // Arrange
        var province = new Province { Id = 1, Name = "Buenos Aires" };
        var city = new City { Id = 1, Name = "La Plata", ProvinceId = 1 };

        // Act
        province.Cities.Add(city);

        // Assert
        Assert.Single(province.Cities);
        Assert.Contains(city, province.Cities);
    }

    [Fact]
    public void Province_ShouldAllowPersonsToBeAdded()
    {
        // Arrange
        var province = new Province { Id = 1, Name = "Buenos Aires" };
        var person = new Person { Id = 1, Name = "John Doe", ProvinceId = 1 };

        // Act
        province.Persons.Add(person);

        // Assert
        Assert.Single(province.Persons);
        Assert.Contains(person, province.Persons);
    }
}
