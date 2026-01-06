using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class CityTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var city = new City();

        // Assert
        Assert.Equal(0, city.Id);
        Assert.Equal(string.Empty, city.Name);
        Assert.True(city.IsActive);
        Assert.InRange(city.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(city.ModifiedDate);
        Assert.Equal("System", city.CreatedBy);
        Assert.Null(city.ModifiedBy);
        Assert.NotNull(city.Persons);
        Assert.Empty(city.Persons);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();

        // Act
        city.Id = 100;

        // Assert
        Assert.Equal(100, city.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();

        // Act
        city.Name = "Buenos Aires";

        // Assert
        Assert.Equal("Buenos Aires", city.Name);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();

        // Act
        city.IsActive = false;

        // Assert
        Assert.False(city.IsActive);
    }

    [Fact]
    public void ProvinceId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();

        // Act
        city.ProvinceId = 5;

        // Assert
        Assert.Equal(5, city.ProvinceId);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();
        var date = new DateTime(2024, 1, 1);

        // Act
        city.CreatedDate = date;

        // Assert
        Assert.Equal(date, city.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();
        var date = new DateTime(2024, 1, 2);

        // Act
        city.ModifiedDate = date;

        // Assert
        Assert.Equal(date, city.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();

        // Act
        city.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", city.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var city = new City();

        // Act
        city.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", city.ModifiedBy);
    }

    [Fact]
    public void Persons_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var city = new City();

        // Assert
        Assert.NotNull(city.Persons);
        Assert.Empty(city.Persons);
        Assert.IsAssignableFrom<ICollection<Person>>(city.Persons);
    }

    [Fact]
    public void City_ShouldAllowPersonsToBeAdded()
    {
        // Arrange
        var city = new City { Id = 1, Name = "Buenos Aires" };
        var person = new Person { Id = 1, Name = "John Doe", CityId = 1 };

        // Act
        city.Persons.Add(person);

        // Assert
        Assert.Single(city.Persons);
        Assert.Contains(person, city.Persons);
    }
}
