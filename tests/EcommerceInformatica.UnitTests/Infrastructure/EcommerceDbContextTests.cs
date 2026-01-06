using Xunit;
using Microsoft.EntityFrameworkCore;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Infrastructure;

public class EcommerceDbContextTests
{
    private DbContextOptions<EcommerceDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_ShouldInitializeDbContext()
    {
        // Arrange
        var options = CreateInMemoryOptions();

        // Act
        using var context = new EcommerceDbContext(options);

        // Assert
        Assert.NotNull(context);
        Assert.NotNull(context.Categories);
        Assert.NotNull(context.Products);
        Assert.NotNull(context.Brands);
        Assert.NotNull(context.Suppliers);
        Assert.NotNull(context.Persons);
        Assert.NotNull(context.Cities);
        Assert.NotNull(context.Provinces);
        Assert.NotNull(context.Invoices);
        Assert.NotNull(context.InvoiceDetails);
        Assert.NotNull(context.PaymentMethods);
    }

    [Fact]
    public void Categories_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var category = new Category { Id = 1, Name = "Test Category" };

        // Act
        context.Categories.Add(category);
        context.SaveChanges();
        var result = context.Categories.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Category", result.Name);
    }

    [Fact]
    public void Products_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var product = new Product { Id = 1, Name = "Test Product", UnitPrice = 100, Stock = 10, CategoryId = 1, BrandId = 1, SupplierId = 1 };

        // Act
        context.Products.Add(product);
        context.SaveChanges();
        var result = context.Products.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public void Brands_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var brand = new Brand { Id = 1, Name = "Test Brand" };

        // Act
        context.Brands.Add(brand);
        context.SaveChanges();
        var result = context.Brands.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Brand", result.Name);
    }

    [Fact]
    public void Suppliers_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var supplier = new Supplier { Id = 1, Name = "Test Supplier" };

        // Act
        context.Suppliers.Add(supplier);
        context.SaveChanges();
        var result = context.Suppliers.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Supplier", result.Name);
    }

    [Fact]
    public void Persons_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var person = new Person { Id = 1, Dni = "12345678", Name = "Test Person", Password = "password" };

        // Act
        context.Persons.Add(person);
        context.SaveChanges();
        var result = context.Persons.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Person", result.Name);
    }

    [Fact]
    public void Cities_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var city = new City { Id = 1, Name = "Test City", ProvinceId = 1 };

        // Act
        context.Cities.Add(city);
        context.SaveChanges();
        var result = context.Cities.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test City", result.Name);
    }

    [Fact]
    public void Provinces_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var province = new Province { Id = 1, Name = "Test Province" };

        // Act
        context.Provinces.Add(province);
        context.SaveChanges();
        var result = context.Provinces.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Province", result.Name);
    }

    [Fact]
    public void Invoices_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var invoice = new Invoice { Id = 1, TotalAmount = 100, PersonId = 1, PaymentMethodId = 1 };

        // Act
        context.Invoices.Add(invoice);
        context.SaveChanges();
        var result = context.Invoices.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(100, result.TotalAmount);
    }

    [Fact]
    public void InvoiceDetails_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var invoiceDetail = new InvoiceDetail { Id = 1, Quantity = 5, UnitPrice = 20, Subtotal = 100, InvoiceId = 1, ProductId = 1 };

        // Act
        context.InvoiceDetails.Add(invoiceDetail);
        context.SaveChanges();
        var result = context.InvoiceDetails.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Quantity);
    }

    [Fact]
    public void PaymentMethods_ShouldAllowAddingAndRetrieving()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var paymentMethod = new PaymentMethod { Id = 1, Name = "Credit Card" };

        // Act
        context.PaymentMethods.Add(paymentMethod);
        context.SaveChanges();
        var result = context.PaymentMethods.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Credit Card", result.Name);
    }
}
