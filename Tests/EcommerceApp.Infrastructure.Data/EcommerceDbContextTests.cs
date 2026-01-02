using Xunit;
using EcommerceApp.Infrastructure.Data;
using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcommerceApp.Infrastructure.Data.Tests;

public class EcommerceDbContextTests
{
    [Fact]
    public void EcommerceDbContext_Constructor_ShouldInitializeWithOptions()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        // Act
        using var context = new EcommerceDbContext(options);

        // Assert
        Assert.NotNull(context);
    }

    [Fact]
    public void EcommerceDbContext_DbSets_ShouldBeInitialized()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_DbSets")
            .Options;

        // Act
        using var context = new EcommerceDbContext(options);

        // Assert
        Assert.NotNull(context.Persons);
        Assert.NotNull(context.Categories);
        Assert.NotNull(context.Brands);
        Assert.NotNull(context.Provinces);
        Assert.NotNull(context.Cities);
        Assert.NotNull(context.PaymentMethods);
        Assert.NotNull(context.Suppliers);
        Assert.NotNull(context.Products);
        Assert.NotNull(context.Invoices);
        Assert.NotNull(context.InvoiceDetails);
    }

    [Fact]
    public void EcommerceDbContext_Persons_ShouldAllowAddingPerson()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddPerson")
            .Options;

        using var context = new EcommerceDbContext(options);
        var person = new Person
        {
            Dni = "12345678",
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            IsActive = true
        };

        // Act
        context.Persons.Add(person);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Persons);
    }

    [Fact]
    public void EcommerceDbContext_Categories_ShouldAllowAddingCategory()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddCategory")
            .Options;

        using var context = new EcommerceDbContext(options);
        var category = new Category
        {
            Name = "Electronics",
            IsActive = true
        };

        // Act
        context.Categories.Add(category);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Categories);
    }

    [Fact]
    public void EcommerceDbContext_Products_ShouldAllowAddingProduct()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddProduct")
            .Options;

        using var context = new EcommerceDbContext(options);
        var product = new Product
        {
            Name = "Laptop",
            Price = 999.99m,
            Stock = 10,
            IsActive = true
        };

        // Act
        context.Products.Add(product);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Products);
    }

    [Fact]
    public void EcommerceDbContext_Brands_ShouldAllowAddingBrand()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddBrand")
            .Options;

        using var context = new EcommerceDbContext(options);
        var brand = new Brand
        {
            Name = "Apple",
            IsActive = true
        };

        // Act
        context.Brands.Add(brand);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Brands);
    }

    [Fact]
    public void EcommerceDbContext_Provinces_ShouldAllowAddingProvince()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddProvince")
            .Options;

        using var context = new EcommerceDbContext(options);
        var province = new Province
        {
            Name = "Buenos Aires",
            IsActive = true
        };

        // Act
        context.Provinces.Add(province);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Provinces);
    }

    [Fact]
    public void EcommerceDbContext_Cities_ShouldAllowAddingCity()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddCity")
            .Options;

        using var context = new EcommerceDbContext(options);
        var city = new City
        {
            Name = "La Plata",
            ProvinceId = 1,
            IsActive = true
        };

        // Act
        context.Cities.Add(city);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Cities);
    }

    [Fact]
    public void EcommerceDbContext_PaymentMethods_ShouldAllowAddingPaymentMethod()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddPaymentMethod")
            .Options;

        using var context = new EcommerceDbContext(options);
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            IsActive = true
        };

        // Act
        context.PaymentMethods.Add(paymentMethod);
        context.SaveChanges();

        // Assert
        Assert.Single(context.PaymentMethods);
    }

    [Fact]
    public void EcommerceDbContext_Suppliers_ShouldAllowAddingSupplier()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddSupplier")
            .Options;

        using var context = new EcommerceDbContext(options);
        var supplier = new Supplier
        {
            Name = "Tech Supplies Inc.",
            Email = "contact@techsupplies.com",
            IsActive = true
        };

        // Act
        context.Suppliers.Add(supplier);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Suppliers);
    }

    [Fact]
    public void EcommerceDbContext_Invoices_ShouldAllowAddingInvoice()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddInvoice")
            .Options;

        using var context = new EcommerceDbContext(options);
        var invoice = new Invoice
        {
            CustomerDni = "12345678",
            Total = 1500.00m,
            IsActive = true
        };

        // Act
        context.Invoices.Add(invoice);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Invoices);
    }

    [Fact]
    public void EcommerceDbContext_InvoiceDetails_ShouldAllowAddingInvoiceDetail()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_AddInvoiceDetail")
            .Options;

        using var context = new EcommerceDbContext(options);
        var invoiceDetail = new InvoiceDetail
        {
            InvoiceId = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50.00m,
            Subtotal = 100.00m
        };

        // Act
        context.InvoiceDetails.Add(invoiceDetail);
        context.SaveChanges();

        // Assert
        Assert.Single(context.InvoiceDetails);
    }
}
