using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class ProductRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<ProductRepository>> _mockLogger;
    private readonly Mock<DbSet<Product>> _mockProductSet;
    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<ProductRepository>>();
        _mockProductSet = new Mock<DbSet<Product>>();
        _repository = new ProductRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new ProductRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new ProductRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 },
            new Product { Id = 2, Name = "Product 2", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 },
            new Product { Id = 3, Name = "Product 3", IsActive = false, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, p => Assert.True(p.IsActive));
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Product>())
            .Throws(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.GetAllAsync());
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsProduct()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Product 1", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var products = new List<Product>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithInactiveProduct_ReturnsNull()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", IsActive = false, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidProduct_SetsCreatedDateAndIsActive()
    {
        // Arrange
        var product = new Product
        {
            Name = "New Product",
            Description = "Description",
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };

        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _repository.AddAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockProductSet.Verify(s => s.AddAsync(product, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        var product = new Product
        {
            Name = "New Product",
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };

        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.AddAsync(product));
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidProduct_SetsModifiedDate()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Updated Product",
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _repository.UpdateAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockProductSet.Verify(s => s.Update(product), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Updated Product",
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };

        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.UpdateAsync(product));
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Product",
            IsActive = true,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };

        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockProductSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _repository.DeleteAsync(1);

        // Assert
        Assert.False(product.IsActive);
        Assert.True(product.ModifiedDate != default);
        _mockProductSet.Verify(s => s.Update(product), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockProductSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Product with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Product>()).Returns(_mockProductSet.Object);
        _mockProductSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.DeleteAsync(1));
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var products = new List<Product>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInactiveId_ReturnsFalse()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", IsActive = false, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.ExistsAsync(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop HP", Description = "High performance", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 },
            new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 },
            new Product { Id = 3, Name = "Keyboard", Description = "HP keyboard", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.SearchAsync("HP");

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Contains(resultList, p => p.Name.Contains("HP", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 },
            new Product { Id = 2, Name = "Product 2", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.SearchAsync("");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", IsActive = true, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(products);
        _mockContext.Setup(c => c.Set<Product>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.SearchAsync("NonExistent");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task SearchAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Product>())
            .Throws(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.SearchAsync("test"));
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

}
