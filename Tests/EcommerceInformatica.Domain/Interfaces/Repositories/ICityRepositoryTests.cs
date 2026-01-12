using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class ICityRepositoryTests
{
    private readonly Mock<ICityRepository> _mockRepository;

    public ICityRepositoryTests()
    {
        _mockRepository = new Mock<ICityRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCities()
    {
        // Arrange
        var expectedCities = new List<City>
        {
            new City { Id = 1, Name = "City 1", Description = "Description 1", IsActive = true, ProvinceId = 1 },
            new City { Id = 2, Name = "City 2", Description = "Description 2", IsActive = true, ProvinceId = 1 }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCities);

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
        var expectedCities = new List<City>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedCities);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCity()
    {
        // Arrange
        var cityId = 1;
        var expectedCity = new City
        {
            Id = cityId,
            Name = "Test City",
            Description = "Test Description",
            IsActive = true,
            ProvinceId = 1
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(cityId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCity);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(cityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cityId, result.Id);
        Assert.Equal("Test City", result.Name);
        Assert.Equal(1, result.ProvinceId);
        _mockRepository.Verify(repo => repo.GetByIdAsync(cityId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((City?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidCity_ShouldReturnAddedCity()
    {
        // Arrange
        var newCity = new City
        {
            Name = "New City",
            Description = "New Description",
            IsActive = true,
            ProvinceId = 1
        };
        var addedCity = new City
        {
            Id = 1,
            Name = "New City",
            Description = "New Description",
            IsActive = true,
            ProvinceId = 1
        };
        _mockRepository.Setup(repo => repo.AddAsync(newCity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedCity);

        // Act
        var result = await _mockRepository.Object.AddAsync(newCity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New City", result.Name);
        Assert.Equal(1, result.ProvinceId);
        _mockRepository.Verify(repo => repo.AddAsync(newCity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidCity_ShouldReturnUpdatedCity()
    {
        // Arrange
        var existingCity = new City
        {
            Id = 1,
            Name = "Original City",
            Description = "Original Description",
            IsActive = true,
            ProvinceId = 1
        };
        var updatedCity = new City
        {
            Id = 1,
            Name = "Updated City",
            Description = "Updated Description",
            IsActive = true,
            ProvinceId = 2
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingCity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedCity);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingCity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated City", result.Name);
        Assert.Equal("Updated Description", result.Description);
        Assert.Equal(2, result.ProvinceId);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingCity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var cityId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(cityId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(cityId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(cityId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingCities()
    {
        // Arrange
        var searchTerm = "City";
        var expectedCities = new List<City>
        {
            new City { Id = 1, Name = "City 1", Description = "Description 1", IsActive = true, ProvinceId = 1 },
            new City { Id = 2, Name = "City 2", Description = "Description 2", IsActive = true, ProvinceId = 1 }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCities);

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
        var expectedCities = new List<City>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCities);

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
        var expectedCities = new List<City>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCities);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
