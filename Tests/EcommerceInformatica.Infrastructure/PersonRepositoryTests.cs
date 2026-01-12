using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EcommerceInformatica.Infrastructure.Tests;

public class PersonRepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<ILogger<PersonRepository>> _mockLogger;
    private readonly Mock<DbSet<Person>> _mockPersonSet;
    private readonly PersonRepository _repository;

    public PersonRepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockLogger = new Mock<ILogger<PersonRepository>>();
        _mockPersonSet = new Mock<DbSet<Person>>();
        _repository = new PersonRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new PersonRepository(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new PersonRepository(_mockContext.Object, null!));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActivePersons()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true },
            new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Dni = "87654321", IsActive = true },
            new Person { Id = 3, FirstName = "Bob", LastName = "Jones", Email = "bob@test.com", Dni = "11111111", IsActive = false }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, p => Assert.True(p.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsPerson()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        var persons = new List<Person>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ValidPerson_SetsCreatedDateAndIsActive()
    {
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Dni = "12345678"
        };

        _mockContext.Setup(c => c.Set<Person>()).Returns(_mockPersonSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.AddAsync(person);

        Assert.NotNull(result);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedDate != default);
        _mockPersonSet.Verify(s => s.AddAsync(person, It.IsAny<CancellationToken>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidPerson_SetsModifiedDate()
    {
        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.updated@test.com",
            Dni = "12345678",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Person>()).Returns(_mockPersonSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.UpdateAsync(person);

        Assert.NotNull(result);
        Assert.True(result.ModifiedDate != default);
        _mockPersonSet.Verify(s => s.Update(person), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_SetsIsActiveFalseAndModifiedDate()
    {
        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Dni = "12345678",
            IsActive = true
        };

        _mockContext.Setup(c => c.Set<Person>()).Returns(_mockPersonSet.Object);
        _mockPersonSet.Setup(s => s.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _repository.DeleteAsync(1);

        Assert.False(person.IsActive);
        Assert.True(person.ModifiedDate != default);
        _mockPersonSet.Verify(s => s.Update(person), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ThrowsInvalidOperationException()
    {
        _mockContext.Setup(c => c.Set<Person>()).Returns(_mockPersonSet.Object);
        _mockPersonSet.Setup(s => s.FindAsync(new object[] { 999 }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _repository.DeleteAsync(999));
        Assert.Contains("Person with id 999 not found", exception.Message);
    }

    [Fact]
    public async Task ExistsAsync_WithValidActiveId_ReturnsTrue()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ReturnsFalse()
    {
        var persons = new List<Person>().AsQueryable();
        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithFirstName_ReturnsMatchingPersons()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true },
            new Person { Id = 2, FirstName = "Johnny", LastName = "Smith", Email = "johnny@test.com", Dni = "87654321", IsActive = true },
            new Person { Id = 3, FirstName = "Jane", LastName = "Doe", Email = "jane@test.com", Dni = "11111111", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("John");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, p => Assert.Contains("John", p.FirstName, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task SearchAsync_WithLastName_ReturnsMatchingPersons()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@test.com", Dni = "87654321", IsActive = true },
            new Person { Id = 3, FirstName = "Bob", LastName = "Smith", Email = "bob@test.com", Dni = "11111111", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("Doe");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, p => Assert.Equal("Doe", p.LastName));
    }

    [Fact]
    public async Task SearchAsync_WithEmail_ReturnsMatchingPersons()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true },
            new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Dni = "87654321", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("test.com");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Contains("test.com", resultList.First().Email);
    }

    [Fact]
    public async Task SearchAsync_WithDni_ReturnsMatchingPersons()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true },
            new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Dni = "87654321", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("12345678");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Equal("12345678", resultList.First().Dni);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActivePersons()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true },
            new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Dni = "87654321", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Dni = "12345678", IsActive = true }
        }.AsQueryable();

        var mockSet = TestHelper.CreateMockDbSet(persons);
        _mockContext.Setup(c => c.Set<Person>()).Returns(mockSet.Object);

        var result = await _repository.SearchAsync("NonExistent");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenExceptionOccurs_LogsErrorAndThrows()
    {
        _mockContext.Setup(c => c.Set<Person>())
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
