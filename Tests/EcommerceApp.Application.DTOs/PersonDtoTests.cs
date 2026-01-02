using Xunit;
using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.DTOs.Tests;

public class PersonDtoTests
{
    [Fact]
    public void PersonDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new PersonDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Dni);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
        Assert.Equal(string.Empty, dto.Phone);
        Assert.Equal(string.Empty, dto.Email);
        Assert.False(dto.IsAdmin);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void PersonDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new PersonDto();

        // Act
        dto.Dni = "12345678";
        dto.FirstName = "John";
        dto.LastName = "Doe";
        dto.Phone = "555-1234";
        dto.Email = "john@example.com";
        dto.IsAdmin = true;
        dto.IsActive = true;

        // Assert
        Assert.Equal("12345678", dto.Dni);
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Doe", dto.LastName);
        Assert.Equal("555-1234", dto.Phone);
        Assert.Equal("john@example.com", dto.Email);
        Assert.True(dto.IsAdmin);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void PersonCreateDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new PersonCreateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Dni);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
        Assert.Equal(string.Empty, dto.Password);
        Assert.Equal(string.Empty, dto.Phone);
        Assert.Equal(string.Empty, dto.Email);
        Assert.False(dto.IsAdmin);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void PersonCreateDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new PersonCreateDto();

        // Act
        dto.Dni = "87654321";
        dto.FirstName = "Jane";
        dto.LastName = "Smith";
        dto.Password = "secure123";
        dto.Phone = "555-9876";
        dto.Email = "jane@example.com";
        dto.IsAdmin = true;
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("87654321", dto.Dni);
        Assert.Equal("Jane", dto.FirstName);
        Assert.Equal("Smith", dto.LastName);
        Assert.Equal("secure123", dto.Password);
        Assert.Equal("555-9876", dto.Phone);
        Assert.Equal("jane@example.com", dto.Email);
        Assert.True(dto.IsAdmin);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void PersonUpdateDto_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var dto = new PersonUpdateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
        Assert.Equal(string.Empty, dto.Phone);
        Assert.Equal(string.Empty, dto.Email);
        Assert.False(dto.IsActive);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void PersonUpdateDto_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var dto = new PersonUpdateDto();

        // Act
        dto.FirstName = "Updated";
        dto.LastName = "Name";
        dto.Phone = "555-0000";
        dto.Email = "updated@example.com";
        dto.IsActive = true;
        dto.ModifiedBy = "System";

        // Assert
        Assert.Equal("Updated", dto.FirstName);
        Assert.Equal("Name", dto.LastName);
        Assert.Equal("555-0000", dto.Phone);
        Assert.Equal("updated@example.com", dto.Email);
        Assert.True(dto.IsActive);
        Assert.Equal("System", dto.ModifiedBy);
    }
}
