using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class ISupplierRepositoryTests
{
    private readonly Mock<ISupplierRepository> _mockRepository;

    public ISupplierRepositoryTests()
    {
        _mockRepository = new Mock<ISupplierRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSuppliers()
    {
        // Arrange
        var expectedSuppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Description 1", ContactName = "John", ContactPhone = "123456", ContactEmail = "john@test.com", IsActive = true },
            new Supplier { Id = 2, Name = "Supplier 2", Description = "Description 2", ContactName = "Jane", ContactPhone = "654321", ContactEmail = "jane@test.com", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSuppliers);

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
        var expectedSuppliers = new List<Supplier>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedSuppliers);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnSupplier()
    {
        // Arrange
        var supplierId = 1;
        var expectedSupplier = new Supplier
        {
            Id = supplierId,
            Name = "Test Supplier",
            Description = "Test Description",
            ContactName = "John Doe",
            ContactPhone = "123456789",
            ContactEmail = "john@test.com",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(supplierId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSupplier);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(supplierId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(supplierId, result.Id);
        Assert.Equal("Test Supplier", result.Name);
        Assert.Equal("John Doe", result.ContactName);
        _mockRepository.Verify(repo => repo.GetByIdAsync(supplierId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Supplier?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidSupplier_ShouldReturnAddedSupplier()
    {
        // Arrange
        var newSupplier = new Supplier
        {
            Name = "New Supplier",
            Description = "New Description",
            ContactName = "Jane Smith",
            ContactPhone = "987654321",
            ContactEmail = "jane@test.com",
            IsActive = true
        };
        var addedSupplier = new Supplier
        {
            Id = 1,
            Name = "New Supplier",
            Description = "New Description",
            ContactName = "Jane Smith",
            ContactPhone = "987654321",
            ContactEmail = "jane@test.com",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.AddAsync(newSupplier, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedSupplier);

        // Act
        var result = await _mockRepository.Object.AddAsync(newSupplier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Supplier", result.Name);
        Assert.Equal("Jane Smith", result.ContactName);
        _mockRepository.Verify(repo => repo.AddAsync(newSupplier, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidSupplier_ShouldReturnUpdatedSupplier()
    {
        // Arrange
        var existingSupplier = new Supplier
        {
            Id = 1,
            Name = "Original Supplier",
            Description = "Original Description",
            ContactName = "Original Contact",
            ContactPhone = "111111111",
            ContactEmail = "original@test.com",
            IsActive = true
        };
        var updatedSupplier = new Supplier
        {
            Id = 1,
            Name = "Updated Supplier",
            Description = "Updated Description",
            ContactName = "Updated Contact",
            ContactPhone = "222222222",
            ContactEmail = "updated@test.com",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingSupplier, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedSupplier);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingSupplier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Supplier", result.Name);
        Assert.Equal("Updated Description", result.Description);
        Assert.Equal("Updated Contact", result.ContactName);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingSupplier, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var supplierId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(supplierId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(supplierId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(supplierId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingSuppliers()
    {
        // Arrange
        var searchTerm = "Supplier";
        var expectedSuppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier 1", Description = "Description 1", ContactName = "John", ContactPhone = "123456", ContactEmail = "john@test.com", IsActive = true },
            new Supplier { Id = 2, Name = "Supplier 2", Description = "Description 2", ContactName = "Jane", ContactPhone = "654321", ContactEmail = "jane@test.com", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSuppliers);

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
        var expectedSuppliers = new List<Supplier>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSuppliers);

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
        var expectedSuppliers = new List<Supplier>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSuppliers);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
