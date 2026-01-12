using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class IInvoiceDetailRepositoryTests
{
    private readonly Mock<IInvoiceDetailRepository> _mockRepository;

    public IInvoiceDetailRepositoryTests()
    {
        _mockRepository = new Mock<IInvoiceDetailRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllInvoiceDetails()
    {
        // Arrange
        var expectedInvoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, Quantity = 5, UnitPrice = 10.00m, Subtotal = 50.00m, IsActive = true, InvoiceId = 1, ProductId = 1 },
            new InvoiceDetail { Id = 2, Quantity = 3, UnitPrice = 20.00m, Subtotal = 60.00m, IsActive = true, InvoiceId = 1, ProductId = 2 }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoiceDetails);

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
        var expectedInvoiceDetails = new List<InvoiceDetail>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedInvoiceDetails);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnInvoiceDetail()
    {
        // Arrange
        var invoiceDetailId = 1;
        var expectedInvoiceDetail = new InvoiceDetail
        {
            Id = invoiceDetailId,
            Quantity = 10,
            UnitPrice = 25.50m,
            Subtotal = 255.00m,
            IsActive = true,
            InvoiceId = 1,
            ProductId = 1
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(invoiceDetailId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoiceDetail);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invoiceDetailId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(invoiceDetailId, result.Id);
        Assert.Equal(10, result.Quantity);
        Assert.Equal(25.50m, result.UnitPrice);
        Assert.Equal(255.00m, result.Subtotal);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invoiceDetailId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((InvoiceDetail?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidInvoiceDetail_ShouldReturnAddedInvoiceDetail()
    {
        // Arrange
        var newInvoiceDetail = new InvoiceDetail
        {
            Quantity = 7,
            UnitPrice = 15.00m,
            Subtotal = 105.00m,
            IsActive = true,
            InvoiceId = 2,
            ProductId = 3
        };
        var addedInvoiceDetail = new InvoiceDetail
        {
            Id = 1,
            Quantity = 7,
            UnitPrice = 15.00m,
            Subtotal = 105.00m,
            IsActive = true,
            InvoiceId = 2,
            ProductId = 3
        };
        _mockRepository.Setup(repo => repo.AddAsync(newInvoiceDetail, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedInvoiceDetail);

        // Act
        var result = await _mockRepository.Object.AddAsync(newInvoiceDetail);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(7, result.Quantity);
        Assert.Equal(105.00m, result.Subtotal);
        _mockRepository.Verify(repo => repo.AddAsync(newInvoiceDetail, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidInvoiceDetail_ShouldReturnUpdatedInvoiceDetail()
    {
        // Arrange
        var existingInvoiceDetail = new InvoiceDetail
        {
            Id = 1,
            Quantity = 5,
            UnitPrice = 10.00m,
            Subtotal = 50.00m,
            IsActive = true,
            InvoiceId = 1,
            ProductId = 1
        };
        var updatedInvoiceDetail = new InvoiceDetail
        {
            Id = 1,
            Quantity = 8,
            UnitPrice = 10.00m,
            Subtotal = 80.00m,
            IsActive = true,
            InvoiceId = 1,
            ProductId = 1
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingInvoiceDetail, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedInvoiceDetail);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingInvoiceDetail);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(8, result.Quantity);
        Assert.Equal(80.00m, result.Subtotal);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingInvoiceDetail, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var invoiceDetailId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(invoiceDetailId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(invoiceDetailId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(invoiceDetailId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingInvoiceDetails()
    {
        // Arrange
        var searchTerm = "Detail";
        var expectedInvoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, Quantity = 5, UnitPrice = 10.00m, Subtotal = 50.00m, IsActive = true, InvoiceId = 1, ProductId = 1 },
            new InvoiceDetail { Id = 2, Quantity = 3, UnitPrice = 20.00m, Subtotal = 60.00m, IsActive = true, InvoiceId = 1, ProductId = 2 }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoiceDetails);

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
        var expectedInvoiceDetails = new List<InvoiceDetail>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoiceDetails);

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
        var expectedInvoiceDetails = new List<InvoiceDetail>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoiceDetails);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
