using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class IProvinceRepositoryTests
{
    private readonly Mock<IProvinceRepository> _mockRepository;

    public IProvinceRepositoryTests()
    {
        _mockRepository = new Mock<IProvinceRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProvinces()
    {
        // Arrange
        var expectedProvinces = new List<Province>
        {
            new Province { Id = 1, Name = "Province 1", Description = "Description 1", IsActive = true },
            new Province { Id = 2, Name = "Province 2", Description = "Description 2", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvinces);

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
        var expectedProvinces = new List<Province>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedProvinces);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnProvince()
    {
        // Arrange
        var provinceId = 1;
        var expectedProvince = new Province
        {
            Id = provinceId,
            Name = "Test Province",
            Description = "Test Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(provinceId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvince);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(provinceId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(provinceId, result.Id);
        Assert.Equal("Test Province", result.Name);
        _mockRepository.Verify(repo => repo.GetByIdAsync(provinceId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Province?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidProvince_ShouldReturnAddedProvince()
    {
        // Arrange
        var newProvince = new Province
        {
            Name = "New Province",
            Description = "New Description",
            IsActive = true
        };
        var addedProvince = new Province
        {
            Id = 1,
            Name = "New Province",
            Description = "New Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.AddAsync(newProvince, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedProvince);

        // Act
        var result = await _mockRepository.Object.AddAsync(newProvince);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Province", result.Name);
        _mockRepository.Verify(repo => repo.AddAsync(newProvince, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidProvince_ShouldReturnUpdatedProvince()
    {
        // Arrange
        var existingProvince = new Province
        {
            Id = 1,
            Name = "Original Province",
            Description = "Original Description",
            IsActive = true
        };
        var updatedProvince = new Province
        {
            Id = 1,
            Name = "Updated Province",
            Description = "Updated Description",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingProvince, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedProvince);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingProvince);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Province", result.Name);
        Assert.Equal("Updated Description", result.Description);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingProvince, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var provinceId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(provinceId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(provinceId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(provinceId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingProvinces()
    {
        // Arrange
        var searchTerm = "Province";
        var expectedProvinces = new List<Province>
        {
            new Province { Id = 1, Name = "Province 1", Description = "Description 1", IsActive = true },
            new Province { Id = 2, Name = "Province 2", Description = "Description 2", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvinces);

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
        var expectedProvinces = new List<Province>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvinces);

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
        var expectedProvinces = new List<Province>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvinces);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
