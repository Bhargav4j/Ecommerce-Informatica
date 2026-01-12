using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class InvoiceRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<InvoiceRepository>> _mockLogger;
    private readonly Mock<DbSet<Invoice>> _mockInvoiceSet;
    private readonly InvoiceRepository _repository;

    public InvoiceRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<InvoiceRepository>>();
        _mockInvoiceSet = new Mock<DbSet<Invoice>>();
        _repository = new InvoiceRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new InvoiceRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new InvoiceRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveInvoices()
    {
        var invoices = new List<Invoice>
        {
            new Invoice { Id = 1, PersonId = 1, PaymentMethodId = 1, TotalAmount = 100, IsActive = true },
            new Invoice { Id = 2, PersonId = 2, PaymentMethodId = 1, TotalAmount = 200, IsActive = true },
            new Invoice { Id = 3, PersonId = 3, PaymentMethodId = 1, TotalAmount = 300, IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, i => Assert.True(i.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsInvoice()
    {
        var invoices = new List<Invoice>
        {
            new Invoice { Id = 1, PersonId = 1, PaymentMethodId = 1, TotalAmount = 100, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(100, result.TotalAmount);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var invoices = new List<Invoice>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidInvoice_SetsCreatedDateAndIsActive()
    {
        var invoice = new Invoice
        {
            PersonId = 1,
            PaymentMethodId = 1,
            TotalAmount = 150
        };

        _mockContext.Setup(c => c.Set<Invoice>()).Returns(_mockInvoiceSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(invoice);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockInvoiceSet.Verify(s => s.AddAsync(invoice, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidInvoice_SetsModifiedDate()
    {
        var invoice = new Invoice
        {
            Id = 1,
            PersonId = 1,
            PaymentMethodId = 1,
            TotalAmount = 200,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Invoice>()).Returns(_mockInvoiceSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(invoice);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockInvoiceSet.Verify(s => s.Update(invoice), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var invoice = new Invoice
        {
            Id = 1,
            PersonId = 1,
            PaymentMethodId = 1,
            TotalAmount = 100,
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Invoice>()).Returns(_mockInvoiceSet.Object);
        _mockInvoiceSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(invoice.IsActive);
        Assert.True(invoice.ModifiedDate != default);
        _mockInvoiceSet.Verify(s => s.Update(invoice), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(_mockInvoiceSet.Object);
        _mockInvoiceSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Invoice?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Invoice with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var invoices = new List<Invoice>
        {
            new Invoice { Id = 1, PersonId = 1, PaymentMethodId = 1, TotalAmount = 100, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var invoices = new List<Invoice>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithPersonName_ReturnsMatchingInvoices()
    {
        var invoices = new List<Invoice>
        {
            new Invoice
            {
                Id = 1,
                PersonId = 1,
                PaymentMethodId = 1,
                TotalAmount = 100,
                IsActive = true,
                Person = new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678" }
            },
            new Invoice
            {
                Id = 2,
                PersonId = 2,
                PaymentMethodId = 1,
                TotalAmount = 200,
                IsActive = true,
                Person = new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Dni = "87654321" }
            }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("John");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Equal("John", resultList.First().Person?.FirstName);
    }

    [Fact]
    public async Task SearchAsync_WithPersonEmail_ReturnsMatchingInvoices()
    {
        var invoices = new List<Invoice>
        {
            new Invoice
            {
                Id = 1,
                PersonId = 1,
                PaymentMethodId = 1,
                TotalAmount = 100,
                IsActive = true,
                Person = new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678" }
            },
            new Invoice
            {
                Id = 2,
                PersonId = 2,
                PaymentMethodId = 1,
                TotalAmount = 200,
                IsActive = true,
                Person = new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Dni = "87654321" }
            }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("test.com");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Contains("test.com", resultList.First().Person?.Email);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveInvoices()
    {
        var invoices = new List<Invoice>
        {
            new Invoice { Id = 1, PersonId = 1, PaymentMethodId = 1, TotalAmount = 100, IsActive = true },
            new Invoice { Id = 2, PersonId = 2, PaymentMethodId = 1, TotalAmount = 200, IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var invoices = new List<Invoice>
        {
            new Invoice
            {
                Id = 1,
                PersonId = 1,
                PaymentMethodId = 1,
                TotalAmount = 100,
                IsActive = true,
                Person = new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678" }
            }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(invoices);
        _mockContext.Setup(c => c.Set<Invoice>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<Invoice>())
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
