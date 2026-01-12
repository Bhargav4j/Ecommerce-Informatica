using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class ProvinceRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<ProvinceRepository>> _mockLogger;
    private readonly Mock<DbSet<Province>> _mockProvinceSet;
    private readonly ProvinceRepository _repository;

    public ProvinceRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<ProvinceRepository>>();
        _mockProvinceSet = new Mock<DbSet<Province>>();
        _repository = new ProvinceRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ProvinceRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ProvinceRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveProvinces()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true },
            new Province { Id = 2, Name = "Cordoba", Description = "Central province", IsActive = true },
            new Province { Id = 3, Name = "Santa Fe", Description = "Coastal province", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, p => Assert.True(p.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsProvince()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Buenos Aires", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var provinces = new List<Province>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidProvince_SetsCreatedDateAndIsActive()
    {
        var province = new Province
        {
            Name = "Mendoza",
            Description = "Wine region"
        };

        _mockContext.Setup(c => c.Set<Province>()).Returns(_mockProvinceSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(province);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockProvinceSet.Verify(s => s.AddAsync(province, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidProvince_SetsModifiedDate()
    {
        var province = new Province
        {
            Id = 1,
            Name = "Buenos Aires Updated",
            Description = "Updated description",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Province>()).Returns(_mockProvinceSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(province);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockProvinceSet.Verify(s => s.Update(province), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var province = new Province
        {
            Id = 1,
            Name = "Buenos Aires",
            Description = "Capital province",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Province>()).Returns(_mockProvinceSet.Object);
        _mockProvinceSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(province);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(province.IsActive);
        Assert.True(province.ModifiedDate != default);
        _mockProvinceSet.Verify(s => s.Update(province), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<Province>()).Returns(_mockProvinceSet.Object);
        _mockProvinceSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Province?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Province with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var provinces = new List<Province>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingProvinces()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true },
            new Province { Id = 2, Name = "Cordoba", Description = "Central Argentina", IsActive = true },
            new Province { Id = 3, Name = "Santa Fe", Description = "Coastal province", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Aires");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Contains("Aires", resultList.First().Name);
    }

    [Fact]
    public async Task SearchAsync_WithDescription_ReturnsMatchingProvinces()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true },
            new Province { Id = 2, Name = "Cordoba", Description = "Central province", IsActive = true },
            new Province { Id = 3, Name = "Santa Fe", Description = "Coastal province", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("province");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(3, resultList.Count);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveProvinces()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true },
            new Province { Id = 2, Name = "Cordoba", Description = "Central province", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Buenos Aires", Description = "Capital province", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(provinces);
        _mockContext.Setup(c => c.Set<Province>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<Province>())
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
