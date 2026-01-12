using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class CategoryRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<CategoryRepository>> _mockLogger;
    private readonly Mock<DbSet<Category>> _mockCategorySet;
    private readonly CategoryRepository _repository;

    public CategoryRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<CategoryRepository>>();
        _mockCategorySet = new Mock<DbSet<Category>>();
        _repository = new CategoryRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new CategoryRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new CategoryRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Desc 1", IsActive = true },
            new Category { Id = 2, Name = "Category 2", Description = "Desc 2", IsActive = true },
            new Category { Id = 3, Name = "Category 3", Description = "Desc 3", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, c => Assert.True(c.IsActive));
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Category>())
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
    public async Task GetByIdAsync_WithValidId_ReturnsCategory()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Desc 1", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Category 1", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var categories = new List<Category>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithInactiveCategory_ReturnsNull()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Desc 1", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidCategory_SetsCreatedDateAndIsActive()
    {
        // Arrange
        var category = new Category
        {
            Name = "New Category",
            Description = "Description"
        };

        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _repository.AddAsync(category);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockCategorySet.Verify(s => s.AddAsync(category, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        var category = new Category { Name = "New Category", Description = "Description" };

        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.AddAsync(category));
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
    public async Task UpdateAsync_ValidCategory_SetsModifiedDate()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Updated Category",
            Description = "Updated Description",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _repository.UpdateAsync(category);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockCategorySet.Verify(s => s.Update(category), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Updated Category",
            Description = "Updated Description"
        };

        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.UpdateAsync(category));
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
        var category = new Category
        {
            Id = 1,
            Name = "Category",
            Description = "Description",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockCategorySet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _repository.DeleteAsync(1);

        // Assert
        Assert.False(category.IsActive);
        Assert.True(category.ModifiedDate != default);
        _mockCategorySet.Verify(s => s.Update(category), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockCategorySet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Category with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);
        _mockCategorySet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
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
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Desc 1", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var categories = new List<Category>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInactiveId_ReturnsFalse()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Desc 1", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.ExistsAsync(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true },
            new Category { Id = 2, Name = "Books", Description = "Reading materials", IsActive = true },
            new Category { Id = 3, Name = "Computers", Description = "Electronic computers", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.SearchAsync("Electronic");

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Contains(resultList, c => c.Name.Contains("Electronic", StringComparison.OrdinalIgnoreCase) ||
                                          c.Description.Contains("Electronic", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Desc 1", IsActive = true },
            new Category { Id = 2, Name = "Category 2", Description = "Desc 2", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

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
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Description 1", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(categories);
        _mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

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
        _mockContext.Setup(c => c.Set<Category>())
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
