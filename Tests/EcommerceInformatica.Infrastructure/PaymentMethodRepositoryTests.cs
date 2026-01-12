using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class PaymentMethodRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<PaymentMethodRepository>> _mockLogger;
    private readonly Mock<DbSet<PaymentMethod>> _mockPaymentMethodSet;
    private readonly PaymentMethodRepository _repository;

    public PaymentMethodRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<PaymentMethodRepository>>();
        _mockPaymentMethodSet = new Mock<DbSet<PaymentMethod>>();
        _repository = new PaymentMethodRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new PaymentMethodRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new PaymentMethodRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActivePaymentMethods()
    {
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Visa/Mastercard", IsActive = true },
            new PaymentMethod { Id = 2, Name = "Cash", Description = "Cash payment", IsActive = true },
            new PaymentMethod { Id = 3, Name = "Check", Description = "Bank check", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, pm => Assert.True(pm.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsPaymentMethod()
    {
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Visa/Mastercard", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Credit Card", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var paymentMethods = new List<PaymentMethod>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidPaymentMethod_SetsCreatedDateAndIsActive()
    {
        var paymentMethod = new PaymentMethod
        {
            Name = "PayPal",
            Description = "Online payment"
        };

        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(_mockPaymentMethodSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(paymentMethod);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockPaymentMethodSet.Verify(s => s.AddAsync(paymentMethod, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidPaymentMethod_SetsModifiedDate()
    {
        var paymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card Updated",
            Description = "All credit cards",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(_mockPaymentMethodSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(paymentMethod);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockPaymentMethodSet.Verify(s => s.Update(paymentMethod), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var paymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "Visa/Mastercard",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(_mockPaymentMethodSet.Object);
        _mockPaymentMethodSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(paymentMethod);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(paymentMethod.IsActive);
        Assert.True(paymentMethod.ModifiedDate != default);
        _mockPaymentMethodSet.Verify(s => s.Update(paymentMethod), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(_mockPaymentMethodSet.Object);
        _mockPaymentMethodSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PaymentMethod?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Payment method with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Visa/Mastercard", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var paymentMethods = new List<PaymentMethod>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithValidTerm_ReturnsMatchingPaymentMethods()
    {
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Visa/Mastercard", IsActive = true },
            new PaymentMethod { Id = 2, Name = "Debit Card", Description = "Bank debit card", IsActive = true },
            new PaymentMethod { Id = 3, Name = "Cash", Description = "Cash payment", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Card");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, pm => Assert.Contains("Card", pm.Name));
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActivePaymentMethods()
    {
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Visa/Mastercard", IsActive = true },
            new PaymentMethod { Id = 2, Name = "Cash", Description = "Cash payment", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Visa/Mastercard", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(paymentMethods);
        _mockContext.Setup(c => c.Set<PaymentMethod>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<PaymentMethod>())
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
