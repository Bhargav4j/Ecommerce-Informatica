using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class CityRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<CityRepository>> _mockLogger;
    private readonly Mock<DbSet<City>> _mockCitySet;
    private readonly CityRepository _repository;

    public CityRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<CityRepository>>();
        _mockCitySet = new Mock<DbSet<City>>();
        _repository = new CityRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CityRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CityRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveCities()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true },
            new City { Id = 2, Name = "Mar del Plata", Description = "Coastal city", ProvinceId = 1, IsActive = true },
            new City { Id = 3, Name = "Bahia Blanca", Description = "Port city", ProvinceId = 1, IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, c => Assert.True(c.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsCity()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("La Plata", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var cities = new List<City>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidCity_SetsCreatedDateAndIsActive()
    {
        var city = new City
        {
            Name = "Rosario",
            Description = "Industrial city",
            ProvinceId = 1
        };

        _mockContext.Setup(c => c.Set<City>()).Returns(_mockCitySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(city);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockCitySet.Verify(s => s.AddAsync(city, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidCity_SetsModifiedDate()
    {
        var city = new City
        {
            Id = 1,
            Name = "La Plata Updated",
            Description = "Updated description",
            ProvinceId = 1,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<City>()).Returns(_mockCitySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(city);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockCitySet.Verify(s => s.Update(city), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var city = new City
        {
            Id = 1,
            Name = "La Plata",
            Description = "Capital city",
            ProvinceId = 1,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<City>()).Returns(_mockCitySet.Object);
        _mockCitySet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(city.IsActive);
        Assert.True(city.ModifiedDate != default);
        _mockCitySet.Verify(s => s.Update(city), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<City>()).Returns(_mockCitySet.Object);
        _mockCitySet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((City?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("City with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var cities = new List<City>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingCities()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true },
            new City { Id = 2, Name = "Mar del Plata", Description = "Coastal city", ProvinceId = 1, IsActive = true },
            new City { Id = 3, Name = "Cordoba", Description = "Central city", ProvinceId = 2, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Plata");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, c => Assert.Contains("Plata", c.Name));
    }

    [Fact]
    public async Task SearchAsync_WithDescription_ReturnsMatchingCities()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true },
            new City { Id = 2, Name = "Mar del Plata", Description = "Coastal city", ProvinceId = 1, IsActive = true },
            new City { Id = 3, Name = "Cordoba", Description = "Central city", ProvinceId = 2, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("city");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(3, resultList.Count);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveCities()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true },
            new City { Id = 2, Name = "Mar del Plata", Description = "Coastal city", ProvinceId = 1, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var cities = new List<City>
        {
            new City { Id = 1, Name = "La Plata", Description = "Capital city", ProvinceId = 1, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(cities);
        _mockContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<City>())
            .Throws(new Exception("Database error"));

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
}
