using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Repositories;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Repositories;

public class ProductRepositoryTests
{
    private readonly Mock<ILogger<ProductRepository>> _mockLogger;

    public ProductRepositoryTests()
    {
        _mockLogger = new Mock<ILogger<ProductRepository>>();
    }

    private DbContextOptions<EcommerceDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActiveProducts()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        context.Products.Add(new Product { Id = 1, Name = "Product1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Product2", IsActive = false, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Product1", result.First().Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        context.Products.Add(new Product { Id = 1, Name = "Product1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Product1", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenDoesNotExist()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProduct()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);
        var product = new Product { Name = "NewProduct", UnitPrice = 100, Stock = 10, CategoryId = 1, BrandId = 1, SupplierId = 1, CreatedBy = "Admin" };

        // Act
        var result = await repository.AddAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("NewProduct", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        var product = new Product { Id = 1, Name = "Product1", UnitPrice = 100, Stock = 10, IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        product.Name = "UpdatedProduct";

        // Act
        await repository.UpdateAsync(product);
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("UpdatedProduct", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteProduct()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        var product = new Product { Id = 1, Name = "Product1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(1);
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenProductExists()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        context.Products.Add(new Product { Id = 1, Name = "Product1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingProducts()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        context.Products.Add(new Product { Id = 1, Name = "Laptop", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Mouse", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.SearchAsync("Lap");

        // Assert
        Assert.Single(result);
        Assert.Equal("Laptop", result.First().Name);
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldReturnProductsInCategory()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new ProductRepository(context, _mockLogger.Object);

        context.Products.Add(new Product { Id = 1, Name = "Product1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 });
        context.Products.Add(new Product { Id = 2, Name = "Product2", IsActive = true, CategoryId = 2, BrandId = 1, SupplierId = 1 });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByCategoryAsync(1);

        // Assert
        Assert.Single(result);
        Assert.Equal("Product1", result.First().Name);
    }
}
