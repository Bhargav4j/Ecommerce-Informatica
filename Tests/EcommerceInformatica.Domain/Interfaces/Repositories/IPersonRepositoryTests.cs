using Moq;
using Xunit;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.Domain.Tests.Interfaces.Repositories;

public class IPersonRepositoryTests
{
    private readonly Mock<IPersonRepository> _mockRepository;

    public IPersonRepositoryTests()
    {
        _mockRepository = new Mock<IPersonRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPersons()
    {
        // Arrange
        var expectedPersons = new List<Person>
        {
            new Person { Id = 1, Dni = "12345678", FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "123456", IsActive = true },
            new Person { Id = 2, Dni = "87654321", FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Phone = "654321", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPersons);

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
        var expectedPersons = new List<Person>();
        _mockRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
            .ReturnsAsync(expectedPersons);

        // Act
        var result = await _mockRepository.Object.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnPerson()
    {
        // Arrange
        var personId = 1;
        var expectedPerson = new Person
        {
            Id = personId,
            Dni = "12345678",
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Phone = "123456789",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(personId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPerson);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(personId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(personId, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        _mockRepository.Verify(repo => repo.GetByIdAsync(personId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidId = 999;
        _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person?)null);

        // Act
        var result = await _mockRepository.Object.GetByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_WithValidPerson_ShouldReturnAddedPerson()
    {
        // Arrange
        var newPerson = new Person
        {
            Dni = "11111111",
            FirstName = "Bob",
            LastName = "Johnson",
            Email = "bob@test.com",
            Phone = "111222333",
            IsActive = true
        };
        var addedPerson = new Person
        {
            Id = 1,
            Dni = "11111111",
            FirstName = "Bob",
            LastName = "Johnson",
            Email = "bob@test.com",
            Phone = "111222333",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.AddAsync(newPerson, It.IsAny<CancellationToken>()))
            .ReturnsAsync(addedPerson);

        // Act
        var result = await _mockRepository.Object.AddAsync(newPerson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Bob", result.FirstName);
        Assert.Equal("Johnson", result.LastName);
        _mockRepository.Verify(repo => repo.AddAsync(newPerson, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidPerson_ShouldReturnUpdatedPerson()
    {
        // Arrange
        var existingPerson = new Person
        {
            Id = 1,
            Dni = "12345678",
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Phone = "123456",
            IsActive = true
        };
        var updatedPerson = new Person
        {
            Id = 1,
            Dni = "12345678",
            FirstName = "Johnny",
            LastName = "Doe",
            Email = "johnny@test.com",
            Phone = "987654",
            IsActive = true
        };
        _mockRepository.Setup(repo => repo.UpdateAsync(existingPerson, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedPerson);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(existingPerson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Johnny", result.FirstName);
        Assert.Equal("johnny@test.com", result.Email);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingPerson, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        var personId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(personId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _mockRepository.Object.DeleteAsync(personId);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(personId, It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task SearchAsync_WithValidSearchTerm_ShouldReturnMatchingPersons()
    {
        // Arrange
        var searchTerm = "John";
        var expectedPersons = new List<Person>
        {
            new Person { Id = 1, Dni = "12345678", FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "123456", IsActive = true },
            new Person { Id = 2, Dni = "11111111", FirstName = "Johnny", LastName = "Smith", Email = "johnny@test.com", Phone = "654321", IsActive = true }
        };
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPersons);

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
        var expectedPersons = new List<Person>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPersons);

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
        var expectedPersons = new List<Person>();
        _mockRepository.Setup(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPersons);

        // Act
        var result = await _mockRepository.Object.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(repo => repo.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }
}
