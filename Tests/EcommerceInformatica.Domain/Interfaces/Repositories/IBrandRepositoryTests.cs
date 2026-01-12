using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class IBrandRepositoryTests
{
    private readonly Mock<IBrandRepository> _mockRepository;

    public IBrandRepositoryTests()
    {
        _mockRepository = new Mock<IBrandRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBrands()
    {
        // Arrange
        var expectedBrands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Description 1", IsActive = true },
            new Brand { Id = 2, Name = "Brand 2", Description = "Description 2", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrands);

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
        var expectedBrands = new List<Brand>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedBrands);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnBrand()
    {
        // Arrange
        var brandId = 1;
        var expectedBrand = new Brand
        {
            Id = brandId,
            Name = "Test Brand",
            Description = "Test Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrand);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(brandId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(brandId, result.Id);
        Assert.Equal("Test Brand", result.Name);
        _mockRepository.Verify(repo => repo.GetByIdAsync(brandId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Brand?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidBrand_ShouldReturnAddedBrand()
    {
        // Arrange
        var newBrand = new Brand
        {
            Name = "New Brand",
            Description = "New Description",
            IsActive = true
        };
        var addedBrand = new Brand
        {
            Id = 1,
            Name = "New Brand",
            Description = "New Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.AddAsync(newBrand, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedBrand);

        // Act
        var result = await _mockRepository.Object.AddAsync(newBrand);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Brand", result.Name);
        _mockRepository.Verify(repo => repo.AddAsync(newBrand, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidBrand_ShouldReturnUpdatedBrand()
    {
        // Arrange
        var existingBrand = new Brand
        {
            Id = 1,
            Name = "Original Brand",
            Description = "Original Description",
            IsActive = true
        };
        var updatedBrand = new Brand
        {
            Id = 1,
            Name = "Updated Brand",
            Description = "Updated Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingBrand, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedBrand);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingBrand);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Brand", result.Name);
        Assert.Equal("Updated Description", result.Description);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingBrand, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var brandId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(brandId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(brandId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(brandId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingBrands()
    {
        // Arrange
        var searchTerm = "Brand";
        var expectedBrands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Description 1", IsActive = true },
            new Brand { Id = 2, Name = "Brand 2", Description = "Description 2", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrands);

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
        var expectedBrands = new List<Brand>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrands);

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
        var expectedBrands = new List<Brand>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrands);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
