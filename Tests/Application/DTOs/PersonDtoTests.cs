using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class PersonDtoTests
{
    [Fact]
    public void PersonDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetDni()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "12345678";

        // Act
        dto.Dni = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Dni);
    }

    [Fact]
    public void PersonDto_ShouldInitializeDniWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.Equal(string.Empty, dto.Dni);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetFirstName()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "John";

        // Act
        dto.FirstName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.FirstName);
    }

    [Fact]
    public void PersonDto_ShouldInitializeFirstNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.Equal(string.Empty, dto.FirstName);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetLastName()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "Doe";

        // Act
        dto.LastName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.LastName);
    }

    [Fact]
    public void PersonDto_ShouldInitializeLastNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.Equal(string.Empty, dto.LastName);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetEmail()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "john.doe@example.com";

        // Act
        dto.Email = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Email);
    }

    [Fact]
    public void PersonDto_ShouldInitializeEmailWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.Equal(string.Empty, dto.Email);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetPhone()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "+1234567890";

        // Act
        dto.Phone = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Phone);
    }

    [Fact]
    public void PersonDto_ShouldInitializePhoneWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.Equal(string.Empty, dto.Phone);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetIsAdmin()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = true;

        // Act
        dto.IsAdmin = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsAdmin);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void PersonDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new PersonDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void PersonDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void PersonDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new PersonDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void PersonDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new PersonDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void PersonDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PersonDto();
        var now = DateTime.Now;

        // Act
        dto.Id = 1;
        dto.Dni = "12345678";
        dto.FirstName = "John";
        dto.LastName = "Doe";
        dto.Email = "john.doe@example.com";
        dto.Phone = "+1234567890";
        dto.IsAdmin = true;
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("12345678", dto.Dni);
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Doe", dto.LastName);
        Assert.Equal("john.doe@example.com", dto.Email);
        Assert.Equal("+1234567890", dto.Phone);
        Assert.True(dto.IsAdmin);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
    }
}
