using Xunit;
using EcommerceApp.Infrastructure.Repositories;
using EcommerceApp.Infrastructure.Data;
using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;

namespace EcommerceApp.Infrastructure.Repositories.Tests;

public class ProductRepositoryTests
{
    private readonly Mock<ILogger<ProductRepository>> _loggerMock;
    private readonly DbContextOptions<EcommerceDbContext> _options;

    public ProductRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<ProductRepository>>();
        _options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActiveProducts()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Test", IsActive = true };
        var brand = new Brand { Id = 1, Name = "Test", IsActive = true };
        var supplier = new Supplier { Id = 1, Name = "Test", IsActive = true };
        context.Categories.Add(category);
        context.Brands.Add(brand);
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();

        context.Products.Add(new Product { Id = 1, Name = "Active", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Inactive", IsActive = false, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Test", IsActive = true };
        var brand = new Brand { Id = 1, Name = "Test", IsActive = true };
        var supplier = new Supplier { Id = 1, Name = "Test", IsActive = true };
        context.Categories.Add(category);
        context.Brands.Add(brand);
        context.Suppliers.Add(supplier);

        var product = new Product { Id = 1, Name = "Laptop", Price = 999.99m, IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProduct()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new ProductRepository(context, _loggerMock.Object);
        var product = new Product { Name = "Phone", Price = 599.99m, IsActive = true };

        // Act
        var result = await repository.AddAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Phone", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var product = new Product { Id = 1, Name = "OldName", Price = 100m, IsActive = true };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);
        product.Name = "NewName";

        // Act
        await repository.UpdateAsync(product);

        // Assert
        var updated = await context.Products.FindAsync(1);
        Assert.Equal("NewName", updated?.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSetIsActiveToFalse()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var product = new Product { Id = 1, Name = "ToDelete", IsActive = true };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        await repository.DeleteAsync(1);

        // Assert
        var deleted = await context.Products.FindAsync(1);
        Assert.False(deleted?.IsActive);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var product = new Product { Id = 1, Name = "Exists", IsActive = true };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingProducts()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Test", IsActive = true };
        var brand = new Brand { Id = 1, Name = "Test", IsActive = true };
        var supplier = new Supplier { Id = 1, Name = "Test", IsActive = true };
        context.Categories.Add(category);
        context.Brands.Add(brand);
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();

        context.Products.Add(new Product { Id = 1, Name = "Laptop Dell", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Phone Samsung", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.SearchAsync("Laptop");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldReturnProductsInCategory()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category1 = new Category { Id = 1, Name = "Electronics", IsActive = true };
        var category2 = new Category { Id = 2, Name = "Books", IsActive = true };
        var brand = new Brand { Id = 1, Name = "Test", IsActive = true };
        var supplier = new Supplier { Id = 1, Name = "Test", IsActive = true };
        context.Categories.AddRange(category1, category2);
        context.Brands.Add(brand);
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();

        context.Products.Add(new Product { Id = 1, Name = "Laptop", CategoryId = 1, IsActive = true, BrandId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Novel", CategoryId = 2, IsActive = true, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByCategoryAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByBrandAsync_ShouldReturnProductsInBrand()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Test", IsActive = true };
        var brand1 = new Brand { Id = 1, Name = "Apple", IsActive = true };
        var brand2 = new Brand { Id = 2, Name = "Samsung", IsActive = true };
        var supplier = new Supplier { Id = 1, Name = "Test", IsActive = true };
        context.Categories.Add(category);
        context.Brands.AddRange(brand1, brand2);
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();

        context.Products.Add(new Product { Id = 1, Name = "iPhone", BrandId = 1, IsActive = true, CategoryId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Galaxy", BrandId = 2, IsActive = true, CategoryId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByBrandAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
