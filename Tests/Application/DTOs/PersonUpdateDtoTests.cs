using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class PersonUpdateDtoTests
{
    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetDni()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = "12345678";

        // Act
        dto.Dni = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Dni);
    }

    [Fact]
    public void PersonUpdateDto_ShouldInitializeDniWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Dni);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetFirstName()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = "John";

        // Act
        dto.FirstName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.FirstName);
    }

    [Fact]
    public void PersonUpdateDto_ShouldInitializeFirstNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.FirstName);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetLastName()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = "Doe";

        // Act
        dto.LastName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.LastName);
    }

    [Fact]
    public void PersonUpdateDto_ShouldInitializeLastNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.LastName);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetEmail()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = "john.doe@example.com";

        // Act
        dto.Email = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Email);
    }

    [Fact]
    public void PersonUpdateDto_ShouldInitializeEmailWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Email);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetPhone()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = "+1234567890";

        // Act
        dto.Phone = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Phone);
    }

    [Fact]
    public void PersonUpdateDto_ShouldInitializePhoneWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Phone);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetIsAdmin()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsAdmin = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsAdmin);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new PersonUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void PersonUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PersonUpdateDto();

        // Act
        dto.Dni = "12345678";
        dto.FirstName = "John";
        dto.LastName = "Doe";
        dto.Email = "john.doe@example.com";
        dto.Phone = "+1234567890";
        dto.IsAdmin = true;
        dto.IsActive = true;

        // Assert
        Assert.Equal("12345678", dto.Dni);
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Doe", dto.LastName);
        Assert.Equal("john.doe@example.com", dto.Email);
        Assert.Equal("+1234567890", dto.Phone);
        Assert.True(dto.IsAdmin);
        Assert.True(dto.IsActive);
    }
}
