using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class PersonTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.NotNull(person);
        Assert.Equal(0, person.Id);
        Assert.Equal(string.Empty, person.Dni);
        Assert.Equal(string.Empty, person.FirstName);
        Assert.Equal(string.Empty, person.LastName);
        Assert.Equal(string.Empty, person.Email);
        Assert.Equal(string.Empty, person.Phone);
        Assert.Equal(string.Empty, person.PasswordHash);
        Assert.False(person.IsAdmin);
        Assert.False(person.IsActive);
        Assert.Equal(string.Empty, person.CreatedBy);
        Assert.Null(person.ModifiedBy);
        Assert.NotNull(person.Invoices);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Id = 200;

        // Assert
        Assert.Equal(200, person.Id);
    }

    [Fact]
    public void SetDni_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Dni = "12345678A";

        // Assert
        Assert.Equal("12345678A", person.Dni);
    }

    [Fact]
    public void SetFirstName_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.FirstName = "Jane";

        // Assert
        Assert.Equal("Jane", person.FirstName);
    }

    [Fact]
    public void SetLastName_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.LastName = "Smith";

        // Assert
        Assert.Equal("Smith", person.LastName);
    }

    [Fact]
    public void SetEmail_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Email = "jane.smith@example.com";

        // Assert
        Assert.Equal("jane.smith@example.com", person.Email);
    }

    [Fact]
    public void SetPhone_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Phone = "+1234567890";

        // Assert
        Assert.Equal("+1234567890", person.Phone);
    }

    [Fact]
    public void SetPasswordHash_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.PasswordHash = "hashedpassword123";

        // Assert
        Assert.Equal("hashedpassword123", person.PasswordHash);
    }

    [Fact]
    public void SetIsAdmin_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.IsAdmin = true;

        // Assert
        Assert.True(person.IsAdmin);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.IsActive = true;

        // Assert
        Assert.True(person.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var person = new Person();
        var date = new DateTime(2026, 1, 12);

        // Act
        person.CreatedDate = date;

        // Assert
        Assert.Equal(date, person.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var person = new Person();
        var date = new DateTime(2026, 1, 12);

        // Act
        person.ModifiedDate = date;

        // Assert
        Assert.Equal(date, person.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var person = new Person();

        // Act
        person.ModifiedDate = null;

        // Assert
        Assert.Null(person.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.CreatedBy = "system";

        // Assert
        Assert.Equal("system", person.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var person = new Person();

        // Act
        person.ModifiedBy = "admin";

        // Assert
        Assert.Equal("admin", person.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var person = new Person();

        // Act
        person.ModifiedBy = null;

        // Assert
        Assert.Null(person.ModifiedBy);
    }

    [Fact]
    public void SetInvoices_ShouldSetCollection()
    {
        // Arrange
        var person = new Person();
        var invoices = new List<Invoice>
        {
            new Invoice { Id = 1, InvoiceDate = DateTime.Now, TotalAmount = 100m },
            new Invoice { Id = 2, InvoiceDate = DateTime.Now, TotalAmount = 200m }
        };

        // Act
        person.Invoices = invoices;

        // Assert
        Assert.NotNull(person.Invoices);
        Assert.Equal(2, person.Invoices.Count);
    }

    [Fact]
    public void Invoices_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.NotNull(person.Invoices);
        Assert.Empty(person.Invoices);
    }
}
