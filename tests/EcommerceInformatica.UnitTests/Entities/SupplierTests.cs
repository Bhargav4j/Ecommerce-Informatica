using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class SupplierTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var supplier = new Supplier();

        // Assert
        Assert.Equal(0, supplier.Id);
        Assert.Equal(string.Empty, supplier.Name);
        Assert.Null(supplier.Email);
        Assert.Null(supplier.Phone);
        Assert.Null(supplier.Address);
        Assert.Null(supplier.City);
        Assert.Null(supplier.Province);
        Assert.True(supplier.IsActive);
        Assert.InRange(supplier.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(supplier.ModifiedDate);
        Assert.Equal(string.Empty, supplier.CreatedBy);
        Assert.Null(supplier.ModifiedBy);
        Assert.NotNull(supplier.Products);
        Assert.Empty(supplier.Products);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Id = 100;

        // Assert
        Assert.Equal(100, supplier.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Name = "Tech Supplier Inc";

        // Assert
        Assert.Equal("Tech Supplier Inc", supplier.Name);
    }

    [Fact]
    public void Email_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Email = "contact@techsupplier.com";

        // Assert
        Assert.Equal("contact@techsupplier.com", supplier.Email);
    }

    [Fact]
    public void Phone_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Phone = "123-456-7890";

        // Assert
        Assert.Equal("123-456-7890", supplier.Phone);
    }

    [Fact]
    public void Address_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Address = "123 Main St";

        // Assert
        Assert.Equal("123 Main St", supplier.Address);
    }

    [Fact]
    public void City_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.City = "New York";

        // Assert
        Assert.Equal("New York", supplier.City);
    }

    [Fact]
    public void Province_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Province = "NY";

        // Assert
        Assert.Equal("NY", supplier.Province);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.IsActive = false;

        // Assert
        Assert.False(supplier.IsActive);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();
        var date = new DateTime(2024, 1, 1);

        // Act
        supplier.CreatedDate = date;

        // Assert
        Assert.Equal(date, supplier.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();
        var date = new DateTime(2024, 1, 2);

        // Act
        supplier.ModifiedDate = date;

        // Assert
        Assert.Equal(date, supplier.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", supplier.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", supplier.ModifiedBy);
    }

    [Fact]
    public void Products_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var supplier = new Supplier();

        // Assert
        Assert.NotNull(supplier.Products);
        Assert.Empty(supplier.Products);
        Assert.IsAssignableFrom<ICollection<Product>>(supplier.Products);
    }

    [Fact]
    public void Supplier_ShouldAllowProductsToBeAdded()
    {
        // Arrange
        var supplier = new Supplier { Id = 1, Name = "Tech Supplier Inc" };
        var product = new Product { Id = 1, Name = "Laptop", SupplierId = 1 };

        // Act
        supplier.Products.Add(product);

        // Assert
        Assert.Single(supplier.Products);
        Assert.Contains(product, supplier.Products);
    }

    [Fact]
    public void Supplier_ShouldHandleMultipleProducts()
    {
        // Arrange
        var supplier = new Supplier { Id = 1, Name = "Tech Supplier Inc" };
        var product1 = new Product { Id = 1, Name = "Laptop", SupplierId = 1 };
        var product2 = new Product { Id = 2, Name = "Mouse", SupplierId = 1 };

        // Act
        supplier.Products.Add(product1);
        supplier.Products.Add(product2);

        // Assert
        Assert.Equal(2, supplier.Products.Count);
        Assert.Contains(product1, supplier.Products);
        Assert.Contains(product2, supplier.Products);
    }
}
