using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class IInvoiceRepositoryTests
{
    private readonly Mock<IInvoiceRepository> _mockRepository;

    public IInvoiceRepositoryTests()
    {
        _mockRepository = new Mock<IInvoiceRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllInvoices()
    {
        // Arrange
        var expectedInvoices = new List<Invoice>
        {
            new Invoice { Id = 1, InvoiceDate = DateTime.Now, TotalAmount = 100.00m, IsActive = true, PersonId = 1, PaymentMethodId = 1 },
            new Invoice { Id = 2, InvoiceDate = DateTime.Now, TotalAmount = 200.00m, IsActive = true, PersonId = 2, PaymentMethodId = 2 }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoices);

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
        var expectedInvoices = new List<Invoice>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedInvoices);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnInvoice()
    {
        // Arrange
        var invoiceId = 1;
        var invoiceDate = DateTime.Now;
        var expectedInvoice = new Invoice
        {
            Id = invoiceId,
            InvoiceDate = invoiceDate,
            TotalAmount = 150.00m,
            IsActive = true,
            PersonId = 1,
            PaymentMethodId = 1
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(invoiceId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoice);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invoiceId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(invoiceId, result.Id);
        Assert.Equal(150.00m, result.TotalAmount);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invoiceId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Invoice?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidInvoice_ShouldReturnAddedInvoice()
    {
        // Arrange
        var newInvoice = new Invoice
        {
            InvoiceDate = DateTime.Now,
            TotalAmount = 250.00m,
            IsActive = true,
            PersonId = 1,
            PaymentMethodId = 1
        };
        var addedInvoice = new Invoice
        {
            Id = 1,
            InvoiceDate = newInvoice.InvoiceDate,
            TotalAmount = 250.00m,
            IsActive = true,
            PersonId = 1,
            PaymentMethodId = 1
        };
        _mockRepository.Setup(repo => repo.AddAsync(newInvoice, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedInvoice);

        // Act
        var result = await _mockRepository.Object.AddAsync(newInvoice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(250.00m, result.TotalAmount);
        _mockRepository.Verify(repo => repo.AddAsync(newInvoice, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidInvoice_ShouldReturnUpdatedInvoice()
    {
        // Arrange
        var existingInvoice = new Invoice
        {
            Id = 1,
            InvoiceDate = DateTime.Now,
            TotalAmount = 100.00m,
            IsActive = true,
            PersonId = 1,
            PaymentMethodId = 1
        };
        var updatedInvoice = new Invoice
        {
            Id = 1,
            InvoiceDate = DateTime.Now,
            TotalAmount = 150.00m,
            IsActive = true,
            PersonId = 1,
            PaymentMethodId = 2
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingInvoice, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedInvoice);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingInvoice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(150.00m, result.TotalAmount);
        Assert.Equal(2, result.PaymentMethodId);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingInvoice, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var invoiceId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(invoiceId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(invoiceId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(invoiceId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingInvoices()
    {
        // Arrange
        var searchTerm = "Invoice";
        var expectedInvoices = new List<Invoice>
        {
            new Invoice { Id = 1, InvoiceDate = DateTime.Now, TotalAmount = 100.00m, IsActive = true, PersonId = 1, PaymentMethodId = 1 },
            new Invoice { Id = 2, InvoiceDate = DateTime.Now, TotalAmount = 200.00m, IsActive = true, PersonId = 2, PaymentMethodId = 2 }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoices);

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
        var expectedInvoices = new List<Invoice>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoices);

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
        var expectedInvoices = new List<Invoice>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoices);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
