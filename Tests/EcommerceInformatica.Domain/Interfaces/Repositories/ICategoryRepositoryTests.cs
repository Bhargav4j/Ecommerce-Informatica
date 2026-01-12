using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class ICategoryRepositoryTests
{
    private readonly Mock<ICategoryRepository> _mockRepository;

    public ICategoryRepositoryTests()
    {
        _mockRepository = new Mock<ICategoryRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCategories()
    {
        // Arrange
        var expectedCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Description 1", IsActive = true },
            new Category { Id = 2, Name = "Category 2", Description = "Description 2", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _mockRepository.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_WithCancellationToken_ShouldPassTokenToRepository()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var expectedCategories = new List<Category>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCategory()
    {
        // Arrange
        var categoryId = 1;
        var expectedCategory = new Category
        {
            Id = categoryId,
            Name = "Test Category",
            Description = "Test Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategory);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
        Assert.Equal("Test Category", result.Name);
        _mockRepository.Verify(repo => repo.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidCategory_ShouldReturnAddedCategory()
    {
        // Arrange
        var newCategory = new Category
        {
            Name = "New Category",
            Description = "New Description",
            IsActive = true
        };
        var addedCategory = new Category
        {
            Id = 1,
            Name = "New Category",
            Description = "New Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.AddAsync(newCategory, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedCategory);

        // Act
        var result = await _mockRepository.Object.AddAsync(newCategory);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Category", result.Name);
        _mockRepository.Verify(repo => repo.AddAsync(newCategory, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidCategory_ShouldReturnUpdatedCategory()
    {
        // Arrange
        var existingCategory = new Category
        {
            Id = 1,
            Name = "Original Category",
            Description = "Original Description",
            IsActive = true
        };
        var updatedCategory = new Category
        {
            Id = 1,
            Name = "Updated Category",
            Description = "Updated Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingCategory, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedCategory);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingCategory);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Category", result.Name);
        Assert.Equal("Updated Description", result.Description);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingCategory, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var categoryId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(categoryId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(categoryId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingId = 1;
        _mockRepository.Setup(repo => repo.ExistsAsync(existingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _mockRepository.Object.ExistsAsync(existingId);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(repo => repo.ExistsAsync(existingId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingId = 999;
        _mockRepository.Setup(repo => repo.ExistsAsync(nonExistingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _mockRepository.Object.ExistsAsync(nonExistingId);

        // Assert
        Assert.False(result);
        _mockRepository.Verify(repo => repo.ExistsAsync(nonExistingId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingCategories()
    {
        // Arrange
        var searchTerm = "Category";
        var expectedCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1", Description = "Description 1", IsActive = true },
            new Category { Id = 2, Name = "Category 2", Description = "Description 2", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ShouldReturnEmptyCollection()
    {
        // Arrange
        var searchTerm = "NonExistent";
        var expectedCategories = new List<Category>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_WithEmptySearchTerm_ShouldCallRepository()
    {
        // Arrange
        var searchTerm = string.Empty;
        var expectedCategories = new List<Category>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
