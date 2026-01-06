using Xunit;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.DTOs;

public class SupplierDtoTests
{
    [Fact]
    public void SupplierDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new SupplierDto();

        // Assert
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Email);
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
        Assert.Null(dto.City);
        Assert.Null(dto.Province);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void SupplierDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new SupplierDto();

        // Act
        dto.Id = 100;
        dto.Name = "Tech Supplier Inc";
        dto.Email = "contact@techsupplier.com";
        dto.Phone = "123-456-7890";
        dto.Address = "123 Main St";
        dto.City = "New York";
        dto.Province = "NY";
        dto.IsActive = true;
        dto.CreatedDate = new DateTime(2024, 1, 1);

        // Assert
        Assert.Equal(100, dto.Id);
        Assert.Equal("Tech Supplier Inc", dto.Name);
        Assert.Equal("contact@techsupplier.com", dto.Email);
        Assert.Equal("123-456-7890", dto.Phone);
        Assert.Equal("123 Main St", dto.Address);
        Assert.Equal("New York", dto.City);
        Assert.Equal("NY", dto.Province);
        Assert.True(dto.IsActive);
        Assert.Equal(new DateTime(2024, 1, 1), dto.CreatedDate);
    }

    [Fact]
    public void SupplierCreateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new SupplierCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Email);
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
        Assert.Null(dto.City);
        Assert.Null(dto.Province);
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void SupplierCreateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new SupplierCreateDto();

        // Act
        dto.Name = "Tech Supplier Inc";
        dto.Email = "contact@techsupplier.com";
        dto.Phone = "123-456-7890";
        dto.Address = "123 Main St";
        dto.City = "New York";
        dto.Province = "NY";
        dto.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Tech Supplier Inc", dto.Name);
        Assert.Equal("contact@techsupplier.com", dto.Email);
        Assert.Equal("123-456-7890", dto.Phone);
        Assert.Equal("123 Main St", dto.Address);
        Assert.Equal("New York", dto.City);
        Assert.Equal("NY", dto.Province);
        Assert.Equal("Admin", dto.CreatedBy);
    }

    [Fact]
    public void SupplierUpdateDto_ShouldInitializeProperties()
    {
        // Act
        var dto = new SupplierUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Email);
        Assert.Null(dto.Phone);
        Assert.Null(dto.Address);
        Assert.Null(dto.City);
        Assert.Null(dto.Province);
        Assert.False(dto.IsActive);
        Assert.Equal(string.Empty, dto.ModifiedBy);
    }

    [Fact]
    public void SupplierUpdateDto_ShouldSetAndGetProperties()
    {
        // Arrange
        var dto = new SupplierUpdateDto();

        // Act
        dto.Name = "Tech Supplier Updated";
        dto.Email = "updated@techsupplier.com";
        dto.Phone = "987-654-3210";
        dto.Address = "456 Oak Ave";
        dto.City = "Los Angeles";
        dto.Province = "CA";
        dto.IsActive = true;
        dto.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Tech Supplier Updated", dto.Name);
        Assert.Equal("updated@techsupplier.com", dto.Email);
        Assert.Equal("987-654-3210", dto.Phone);
        Assert.Equal("456 Oak Ave", dto.Address);
        Assert.Equal("Los Angeles", dto.City);
        Assert.Equal("CA", dto.Province);
        Assert.True(dto.IsActive);
        Assert.Equal("Admin", dto.ModifiedBy);
    }
}
