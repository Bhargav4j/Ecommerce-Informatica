using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class IPaymentMethodRepositoryTests
{
    private readonly Mock<IPaymentMethodRepository> _mockRepository;

    public IPaymentMethodRepositoryTests()
    {
        _mockRepository = new Mock<IPaymentMethodRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPaymentMethods()
    {
        // Arrange
        var expectedPaymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Credit Card Payment", IsActive = true },
            new PaymentMethod { Id = 2, Name = "Cash", Description = "Cash Payment", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPaymentMethods);

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
        var expectedPaymentMethods = new List<PaymentMethod>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedPaymentMethods);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnPaymentMethod()
    {
        // Arrange
        var paymentMethodId = 1;
        var expectedPaymentMethod = new PaymentMethod
        {
            Id = paymentMethodId,
            Name = "Credit Card",
            Description = "Credit Card Payment",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(paymentMethodId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPaymentMethod);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(paymentMethodId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(paymentMethodId, result.Id);
        Assert.Equal("Credit Card", result.Name);
        _mockRepository.Verify(repo => repo.GetByIdAsync(paymentMethodId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PaymentMethod?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidPaymentMethod_ShouldReturnAddedPaymentMethod()
    {
        // Arrange
        var newPaymentMethod = new PaymentMethod
        {
            Name = "Debit Card",
            Description = "Debit Card Payment",
            IsActive = true
        };
        var addedPaymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Debit Card",
            Description = "Debit Card Payment",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.AddAsync(newPaymentMethod, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedPaymentMethod);

        // Act
        var result = await _mockRepository.Object.AddAsync(newPaymentMethod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Debit Card", result.Name);
        _mockRepository.Verify(repo => repo.AddAsync(newPaymentMethod, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidPaymentMethod_ShouldReturnUpdatedPaymentMethod()
    {
        // Arrange
        var existingPaymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "Credit Card Payment",
            IsActive = true
        };
        var updatedPaymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "Updated Credit Card Payment",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingPaymentMethod, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedPaymentMethod);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingPaymentMethod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Credit Card Payment", result.Description);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingPaymentMethod, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var paymentMethodId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(paymentMethodId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(paymentMethodId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(paymentMethodId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingPaymentMethods()
    {
        // Arrange
        var searchTerm = "Card";
        var expectedPaymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Credit Card", Description = "Credit Card Payment", IsActive = true },
            new PaymentMethod { Id = 2, Name = "Debit Card", Description = "Debit Card Payment", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPaymentMethods);

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
        var expectedPaymentMethods = new List<PaymentMethod>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPaymentMethods);

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
        var expectedPaymentMethods = new List<PaymentMethod>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPaymentMethods);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
