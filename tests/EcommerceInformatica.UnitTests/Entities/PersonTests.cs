using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class PersonTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var person = new Person();

        // Assert
        Assert.Equal(0, person.Id);
        Assert.Equal(string.Empty, person.Dni);
        Assert.Equal(string.Empty, person.Name);
        Assert.Null(person.Email);
        Assert.Null(person.Phone);
        Assert.Null(person.Address);
        Assert.Equal(string.Empty, person.Password);
        Assert.False(person.IsAdmin);
        Assert.True(person.IsActive);
        Assert.InRange(person.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(person.ModifiedDate);
        Assert.Equal("System", person.CreatedBy);
        Assert.Null(person.ModifiedBy);
        Assert.NotNull(person.Invoices);
        Assert.Empty(person.Invoices);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Id = 100;

        // Assert
        Assert.Equal(100, person.Id);
    }

    [Fact]
    public void Dni_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Dni = "12345678";

        // Assert
        Assert.Equal("12345678", person.Dni);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Name = "John Doe";

        // Assert
        Assert.Equal("John Doe", person.Name);
    }

    [Fact]
    public void Email_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Email = "john.doe@example.com";

        // Assert
        Assert.Equal("john.doe@example.com", person.Email);
    }

    [Fact]
    public void Phone_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Phone = "123-456-7890";

        // Assert
        Assert.Equal("123-456-7890", person.Phone);
    }

    [Fact]
    public void Address_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Address = "123 Main St";

        // Assert
        Assert.Equal("123 Main St", person.Address);
    }

    [Fact]
    public void Password_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Password = "SecurePassword123";

        // Assert
        Assert.Equal("SecurePassword123", person.Password);
    }

    [Fact]
    public void IsAdmin_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.IsAdmin = true;

        // Assert
        Assert.True(person.IsAdmin);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.IsActive = false;

        // Assert
        Assert.False(person.IsActive);
    }

    [Fact]
    public void CityId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.CityId = 5;

        // Assert
        Assert.Equal(5, person.CityId);
    }

    [Fact]
    public void ProvinceId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.ProvinceId = 10;

        // Assert
        Assert.Equal(10, person.ProvinceId);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();
        var date = new DateTime(2024, 1, 1);

        // Act
        person.CreatedDate = date;

        // Assert
        Assert.Equal(date, person.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();
        var date = new DateTime(2024, 1, 2);

        // Act
        person.ModifiedDate = date;

        // Assert
        Assert.Equal(date, person.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", person.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var person = new Person();

        // Act
        person.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", person.ModifiedBy);
    }

    [Fact]
    public void Invoices_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var person = new Person();

        // Assert
        Assert.NotNull(person.Invoices);
        Assert.Empty(person.Invoices);
        Assert.IsAssignableFrom<ICollection<Invoice>>(person.Invoices);
    }

    [Fact]
    public void Person_ShouldAllowInvoicesToBeAdded()
    {
        // Arrange
        var person = new Person { Id = 1, Name = "John Doe" };
        var invoice = new Invoice { Id = 1, PersonId = 1 };

        // Act
        person.Invoices.Add(invoice);

        // Assert
        Assert.Single(person.Invoices);
        Assert.Contains(invoice, person.Invoices);
    }
}
