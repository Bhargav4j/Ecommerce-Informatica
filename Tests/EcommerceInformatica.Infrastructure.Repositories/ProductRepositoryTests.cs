using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Repositories;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Infrastructure.Repositories.Tests;

/// <summary>
/// Test class for ProductRepository
/// </summary>
public class ProductRepositoryTests
{
    private readonly Mock<ILogger<ProductRepository>> _loggerMock;

    public ProductRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<ProductRepository>>();
    }

    private DbContextOptions<EcommerceDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActiveProducts()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        context.Products.Add(new Product { Name = "Product 1", IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Product 2", IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Product 3", IsActive = false, CreatedBy = "Test" });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var products = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, products.Count());
        Assert.All(products, p => Assert.True(p.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var product = new Product { Name = "Test Product", IsActive = true, CreatedBy = "Test" };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);
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
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _loggerMock.Object);

        var product = new Product
        {
            Name = "New Product",
            IsActive = true,
            CreatedBy = "Test",
            UnitPrice = 99.99m,
            Stock = 10
        };

        // Act
        var result = await repository.AddAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Single(context.Products);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var product = new Product { Name = "Original Name", IsActive = true, CreatedBy = "Test" };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);
        product.Name = "Updated Name";

        // Act
        await repository.UpdateAsync(product);

        // Assert
        var updated = await context.Products.FindAsync(product.Id);
        Assert.Equal("Updated Name", updated?.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteProduct()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var product = new Product { Name = "Test Product", IsActive = true, CreatedBy = "Test" };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        await repository.DeleteAsync(product.Id);

        // Assert
        var deleted = await context.Products.FindAsync(product.Id);
        Assert.NotNull(deleted);
        Assert.False(deleted.IsActive);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenProductExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var product = new Product { Name = "Test Product", IsActive = true, CreatedBy = "Test" };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var exists = await repository.ExistsAsync(product.Id);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenProductNotExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var exists = await repository.ExistsAsync(999);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingProducts()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        context.Products.Add(new Product { Name = "Laptop Dell", IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Laptop HP", IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Mouse", IsActive = true, CreatedBy = "Test" });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var results = await repository.SearchAsync("Laptop");

        // Assert
        Assert.Equal(2, results.Count());
        Assert.All(results, p => Assert.Contains("Laptop", p.Name));
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldReturnProductsInCategory()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        context.Products.Add(new Product { Name = "Product 1", CategoryId = 1, IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Product 2", CategoryId = 1, IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Product 3", CategoryId = 2, IsActive = true, CreatedBy = "Test" });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var results = await repository.GetByCategoryAsync(1);

        // Assert
        Assert.Equal(2, results.Count());
        Assert.All(results, p => Assert.Equal(1, p.CategoryId));
    }

    [Fact]
    public async Task GetByBrandAsync_ShouldReturnProductsOfBrand()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        context.Products.Add(new Product { Name = "Product 1", BrandId = 1, IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Product 2", BrandId = 1, IsActive = true, CreatedBy = "Test" });
        context.Products.Add(new Product { Name = "Product 3", BrandId = 2, IsActive = true, CreatedBy = "Test" });
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context, _loggerMock.Object);

        // Act
        var results = await repository.GetByBrandAsync(1);

        // Assert
        Assert.Equal(2, results.Count());
        Assert.All(results, p => Assert.Equal(1, p.BrandId));
    }
}
