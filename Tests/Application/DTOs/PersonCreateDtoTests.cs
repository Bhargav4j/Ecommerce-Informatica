using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class PersonCreateDtoTests
{
    [Fact]
    public void PersonCreateDto_ShouldSetAndGetDni()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = "12345678";

        // Act
        dto.Dni = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Dni);
    }

    [Fact]
    public void PersonCreateDto_ShouldInitializeDniWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Dni);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAndGetFirstName()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = "John";

        // Act
        dto.FirstName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.FirstName);
    }

    [Fact]
    public void PersonCreateDto_ShouldInitializeFirstNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.FirstName);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAndGetLastName()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = "Doe";

        // Act
        dto.LastName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.LastName);
    }

    [Fact]
    public void PersonCreateDto_ShouldInitializeLastNameWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.LastName);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAndGetEmail()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = "john.doe@example.com";

        // Act
        dto.Email = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Email);
    }

    [Fact]
    public void PersonCreateDto_ShouldInitializeEmailWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Email);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAndGetPhone()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = "+1234567890";

        // Act
        dto.Phone = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Phone);
    }

    [Fact]
    public void PersonCreateDto_ShouldInitializePhoneWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Phone);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAndGetPassword()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = "SecurePassword123";

        // Act
        dto.Password = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Password);
    }

    [Fact]
    public void PersonCreateDto_ShouldInitializePasswordWithEmptyString()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Password);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAndGetIsAdmin()
    {
        // Arrange
        var dto = new PersonCreateDto();
        var expectedValue = true;

        // Act
        dto.IsAdmin = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsAdmin);
    }

    [Fact]
    public void PersonCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PersonCreateDto();

        // Act
        dto.Dni = "12345678";
        dto.FirstName = "John";
        dto.LastName = "Doe";
        dto.Email = "john.doe@example.com";
        dto.Phone = "+1234567890";
        dto.Password = "SecurePassword123";
        dto.IsAdmin = true;

        // Assert
        Assert.Equal("12345678", dto.Dni);
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Doe", dto.LastName);
        Assert.Equal("john.doe@example.com", dto.Email);
        Assert.Equal("+1234567890", dto.Phone);
        Assert.Equal("SecurePassword123", dto.Password);
        Assert.True(dto.IsAdmin);
    }
}
