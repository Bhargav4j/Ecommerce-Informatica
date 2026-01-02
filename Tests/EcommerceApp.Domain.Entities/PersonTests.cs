using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class PersonTests
{
    [Fact]
    public void Person_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.NotNull(person);
        Assert.Equal(string.Empty, person.Dni);
        Assert.Equal(string.Empty, person.FirstName);
        Assert.Equal(string.Empty, person.LastName);
        Assert.Equal(string.Empty, person.Password);
        Assert.Equal(string.Empty, person.Phone);
        Assert.Equal(string.Empty, person.Email);
        Assert.False(person.IsAdmin);
        Assert.False(person.IsActive);
        Assert.Equal(string.Empty, person.CreatedBy);
        Assert.Null(person.ModifiedBy);
        Assert.Null(person.ModifiedDate);
        Assert.NotNull(person.Invoices);
        Assert.Empty(person.Invoices);
    }

    [Fact]
    public void Person_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var person = new Person();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);

        // Act
        person.Dni = "12345678";
        person.FirstName = "John";
        person.LastName = "Doe";
        person.Password = "password123";
        person.Phone = "555-1234";
        person.Email = "john.doe@example.com";
        person.IsAdmin = true;
        person.IsActive = true;
        person.CreatedDate = createdDate;
        person.ModifiedDate = modifiedDate;
        person.CreatedBy = "Admin";
        person.ModifiedBy = "System";

        // Assert
        Assert.Equal("12345678", person.Dni);
        Assert.Equal("John", person.FirstName);
        Assert.Equal("Doe", person.LastName);
        Assert.Equal("password123", person.Password);
        Assert.Equal("555-1234", person.Phone);
        Assert.Equal("john.doe@example.com", person.Email);
        Assert.True(person.IsAdmin);
        Assert.True(person.IsActive);
        Assert.Equal(createdDate, person.CreatedDate);
        Assert.Equal(modifiedDate, person.ModifiedDate);
        Assert.Equal("Admin", person.CreatedBy);
        Assert.Equal("System", person.ModifiedBy);
    }

    [Fact]
    public void Person_Invoices_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.NotNull(person.Invoices);
        Assert.IsAssignableFrom<ICollection<Invoice>>(person.Invoices);
        Assert.Empty(person.Invoices);
    }

    [Fact]
    public void Person_Invoices_ShouldAllowAddingInvoices()
    {
        // Arrange
        var person = new Person();
        var invoice = new Invoice { Id = 1, CustomerDni = person.Dni };

        // Act
        person.Invoices.Add(invoice);

        // Assert
        Assert.Single(person.Invoices);
        Assert.Contains(invoice, person.Invoices);
    }

    [Fact]
    public void Person_IsAdmin_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.False(person.IsAdmin);
    }

    [Fact]
    public void Person_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.False(person.IsActive);
    }

    [Fact]
    public void Person_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.Null(person.ModifiedDate);
    }

    [Fact]
    public void Person_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.Null(person.ModifiedBy);
    }

    [Fact]
    public void Person_Email_ShouldAcceptValidEmailFormat()
    {
        // Arrange
        var person = new Person();
        var validEmail = "test@example.com";

        // Act
        person.Email = validEmail;

        // Assert
        Assert.Equal(validEmail, person.Email);
    }

    [Fact]
    public void Person_Password_ShouldStorePasswordAsProvided()
    {
        // Arrange
        var person = new Person();
        var password = "SecurePassword123!";

        // Act
        person.Password = password;

        // Assert
        Assert.Equal(password, person.Password);
    }
}
