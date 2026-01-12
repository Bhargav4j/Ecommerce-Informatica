using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class InvoiceDetailRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<InvoiceDetailRepository>> _mockLogger;
    private readonly Mock<DbSet<InvoiceDetail>> _mockInvoiceDetailSet;
    private readonly InvoiceDetailRepository _repository;

    public InvoiceDetailRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<InvoiceDetailRepository>>();
        _mockInvoiceDetailSet = new Mock<DbSet<InvoiceDetail>>();
        _repository = new InvoiceDetailRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new InvoiceDetailRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new InvoiceDetailRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveInvoiceDetails()
    {
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 2, UnitPrice = 50, IsActive = true },
            new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, Quantity = 1, UnitPrice = 100, IsActive = true },
            new InvoiceDetail { Id = 3, InvoiceId = 2, ProductId = 3, Quantity = 3, UnitPrice = 30, IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, d => Assert.True(d.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsInvoiceDetail()
    {
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 2, UnitPrice = 50, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(2, result.Quantity);
        Assert.Equal(50, result.UnitPrice);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var invoiceDetails = new List<InvoiceDetail>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidInvoiceDetail_SetsCreatedDateAndIsActive()
    {
        var invoiceDetail = new InvoiceDetail
        {
            InvoiceId = 1,
            ProductId = 1,
            Quantity = 3,
            UnitPrice = 75
        };

        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(_mockInvoiceDetailSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(invoiceDetail);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockInvoiceDetailSet.Verify(s => s.AddAsync(invoiceDetail, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidInvoiceDetail_SetsModifiedDate()
    {
        var invoiceDetail = new InvoiceDetail
        {
            Id = 1,
            InvoiceId = 1,
            ProductId = 1,
            Quantity = 5,
            UnitPrice = 80,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(_mockInvoiceDetailSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(invoiceDetail);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockInvoiceDetailSet.Verify(s => s.Update(invoiceDetail), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var invoiceDetail = new InvoiceDetail
        {
            Id = 1,
            InvoiceId = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(_mockInvoiceDetailSet.Object);
        _mockInvoiceDetailSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoiceDetail);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(invoiceDetail.IsActive);
        Assert.True(invoiceDetail.ModifiedDate != default);
        _mockInvoiceDetailSet.Verify(s => s.Update(invoiceDetail), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(_mockInvoiceDetailSet.Object);
        _mockInvoiceDetailSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((InvoiceDetail?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Invoice detail with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 2, UnitPrice = 50, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var invoiceDetails = new List<InvoiceDetail>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithProductName_ReturnsMatchingInvoiceDetails()
    {
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail
            {
                Id = 1,
                InvoiceId = 1,
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 50,
                IsActive = true,
                Product = new Product { Id = 1, Name = "Laptop", Description = "Gaming laptop", CategoryId = 1, BrandId = 1, SupplierId = 1 }
            },
            new InvoiceDetail
            {
                Id = 2,
                InvoiceId = 1,
                ProductId = 2,
                Quantity = 1,
                UnitPrice = 100,
                IsActive = true,
                Product = new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", CategoryId = 1, BrandId = 1, SupplierId = 1 }
            }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Laptop");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Equal("Laptop", resultList.First().Product?.Name);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveInvoiceDetails()
    {
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 2, UnitPrice = 50, IsActive = true },
            new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, Quantity = 1, UnitPrice = 100, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail
            {
                Id = 1,
                InvoiceId = 1,
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 50,
                IsActive = true,
                Product = new Product { Id = 1, Name = "Laptop", Description = "Gaming laptop", CategoryId = 1, BrandId = 1, SupplierId = 1 }
            }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoiceDetails);
        _mockContext.Setup(c => c.Set<InvoiceDetail>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<InvoiceDetail>())
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
