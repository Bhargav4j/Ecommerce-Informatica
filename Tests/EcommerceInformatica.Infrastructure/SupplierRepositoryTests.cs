using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class SupplierRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<SupplierRepository>> _mockLogger;
    private readonly Mock<DbSet<Supplier>> _mockSupplierSet;
    private readonly SupplierRepository _repository;

    public SupplierRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<SupplierRepository>>();
        _mockSupplierSet = new Mock<DbSet<Supplier>>();
        _repository = new SupplierRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new SupplierRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new SupplierRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveSuppliers()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Desc 1", ContactName = "John", ContactEmail = "john@test.com", IsActive = true },
            new Supplier { Id = 2, Name = "Supplier 2", Description = "Desc 2", ContactName = "Jane", ContactEmail = "jane@test.com", IsActive = true },
            new Supplier { Id = 3, Name = "Supplier 3", Description = "Desc 3", ContactName = "Bob", ContactEmail = "bob@test.com", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, s => Assert.True(s.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsSupplier()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Desc 1", ContactName = "John", ContactEmail = "john@test.com", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Supplier 1", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var suppliers = new List<Supplier>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidSupplier_SetsCreatedDateAndIsActive()
    {
        var supplier = new Supplier
        {
            Name = "New Supplier",
            Description = "Description",
            ContactName = "John",
            ContactEmail = "john@test.com"
        };

        _mockContext.Setup(c => c.Set<Supplier>()).Returns(_mockSupplierSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(supplier);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockSupplierSet.Verify(s => s.AddAsync(supplier, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidSupplier_SetsModifiedDate()
    {
        var supplier = new Supplier
        {
            Id = 1,
            Name = "Updated Supplier",
            Description = "Updated Description",
            ContactName = "John",
            ContactEmail = "john@test.com",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Supplier>()).Returns(_mockSupplierSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(supplier);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockSupplierSet.Verify(s => s.Update(supplier), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var supplier = new Supplier
        {
            Id = 1,
            Name = "Supplier",
            Description = "Description",
            ContactName = "John",
            ContactEmail = "john@test.com",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Supplier>()).Returns(_mockSupplierSet.Object);
        _mockSupplierSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(supplier);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(supplier.IsActive);
        Assert.True(supplier.ModifiedDate != default);
        _mockSupplierSet.Verify(s => s.Update(supplier), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(_mockSupplierSet.Object);
        _mockSupplierSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Supplier?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Supplier with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Desc 1", ContactName = "John", ContactEmail = "john@test.com", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var suppliers = new List<Supplier>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingSuppliers()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Tech Supplier", Description = "Technology products", ContactName = "John Smith", ContactEmail = "john@tech.com", IsActive = true },
            new Supplier { Id = 2, Name = "Office Supply Co", Description = "Office materials", ContactName = "Jane Doe", ContactEmail = "jane@office.com", IsActive = true },
            new Supplier { Id = 3, Name = "Hardware Inc", Description = "Tech hardware", ContactName = "Bob Jones", ContactEmail = "bob@hardware.com", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Tech");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
    }

    [Fact]
    public async Task SearchAsync_WithContactName_ReturnsMatchingSuppliers()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Desc 1", ContactName = "John Smith", ContactEmail = "john@test.com", IsActive = true },
            new Supplier { Id = 2, Name = "Supplier 2", Description = "Desc 2", ContactName = "Jane Doe", ContactEmail = "jane@test.com", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("John");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Equal("John Smith", resultList.First().ContactName);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveSuppliers()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Desc 1", ContactName = "John", ContactEmail = "john@test.com", IsActive = true },
            new Supplier { Id = 2, Name = "Supplier 2", Description = "Desc 2", ContactName = "Jane", ContactEmail = "jane@test.com", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Description 1", ContactName = "John", ContactEmail = "john@test.com", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(suppliers);
        _mockContext.Setup(c => c.Set<Supplier>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<Supplier>())
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
