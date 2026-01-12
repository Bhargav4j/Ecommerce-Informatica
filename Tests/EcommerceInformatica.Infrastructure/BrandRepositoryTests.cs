using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class BrandRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<BrandRepository>> _mockLogger;
    private readonly Mock<DbSet<Brand>> _mockBrandSet;
    private readonly BrandRepository _repository;

    public BrandRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<BrandRepository>>();
        _mockBrandSet = new Mock<DbSet<Brand>>();
        _repository = new BrandRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new BrandRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new BrandRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveBrands()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Desc 1", IsActive = true },
            new Brand { Id = 2, Name = "Brand 2", Description = "Desc 2", IsActive = true },
            new Brand { Id = 3, Name = "Brand 3", Description = "Desc 3", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, b => Assert.True(b.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsBrand()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Desc 1", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Brand 1", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var brands = new List<Brand>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidBrand_SetsCreatedDateAndIsActive()
    {
        var brand = new Brand { Name = "New Brand", Description = "Description" };

        _mockContext.Setup(c => c.Set<Brand>()).Returns(_mockBrandSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(brand);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockBrandSet.Verify(s => s.AddAsync(brand, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidBrand_SetsModifiedDate()
    {
        var brand = new Brand
        {
            Id = 1,
            Name = "Updated Brand",
            Description = "Updated Description",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Brand>()).Returns(_mockBrandSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(brand);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockBrandSet.Verify(s => s.Update(brand), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var brand = new Brand
        {
            Id = 1,
            Name = "Brand",
            Description = "Description",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Brand>()).Returns(_mockBrandSet.Object);
        _mockBrandSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(brand);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(brand.IsActive);
        Assert.True(brand.ModifiedDate != default);
        _mockBrandSet.Verify(s => s.Update(brand), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<Brand>()).Returns(_mockBrandSet.Object);
        _mockBrandSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Brand?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Brand with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Desc 1", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var brands = new List<Brand>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingBrands()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Nike", Description = "Sports brand", IsActive = true },
            new Brand { Id = 2, Name = "Adidas", Description = "Athletic wear", IsActive = true },
            new Brand { Id = 3, Name = "Puma", Description = "Sports equipment", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Sports");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveBrands()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Desc 1", IsActive = true },
            new Brand { Id = 2, Name = "Brand 2", Description = "Desc 2", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1", Description = "Description 1", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(brands);
        _mockContext.Setup(c => c.Set<Brand>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<Brand>())
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
