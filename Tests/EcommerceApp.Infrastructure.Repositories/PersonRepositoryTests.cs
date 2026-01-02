using Xunit;
using EcommerceApp.Infrastructure.Repositories;
using EcommerceApp.Infrastructure.Data;
using EcommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;

namespace EcommerceApp.Infrastructure.Repositories.Tests;

public class PersonRepositoryTests
{
    private readonly Mock<ILogger<PersonRepository>> _loggerMock;
    private readonly DbContextOptions<EcommerceDbContext> _options;

    public PersonRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<PersonRepository>>();
        _options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActivePersons()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        context.Persons.Add(new Person { Dni = "1", FirstName = "Active", IsActive = true });
        context.Persons.Add(new Person { Dni = "2", FirstName = "Inactive", IsActive = false });
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByDniAsync_ShouldReturnPerson_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "12345678", FirstName = "John", LastName = "Doe", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByDniAsync("12345678");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task GetByDniAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByDniAsync("999");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnPerson_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "123", Email = "test@example.com", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByEmailAsync("test@example.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByEmailAsync("nonexistent@example.com");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddPerson()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new PersonRepository(context, _loggerMock.Object);
        var person = new Person { Dni = "987", FirstName = "Jane", IsActive = true };

        // Act
        var result = await repository.AddAsync(person);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("987", result.Dni);
        Assert.Equal("Jane", result.FirstName);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdatePerson()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "111", FirstName = "OldName", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);
        person.FirstName = "NewName";

        // Act
        await repository.UpdateAsync(person);

        // Assert
        var updated = await context.Persons.FindAsync("111");
        Assert.Equal("NewName", updated?.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSetIsActiveToFalse()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "222", FirstName = "ToDelete", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        await repository.DeleteAsync("222");

        // Assert
        var deleted = await context.Persons.FindAsync("222");
        Assert.False(deleted?.IsActive);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "333", FirstName = "Exists", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync("333");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync("999");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldReturnPerson_WhenCredentialsValid()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "444", Email = "auth@example.com", Password = "password123", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.AuthenticateAsync("auth@example.com", "password123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("auth@example.com", result.Email);
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldReturnNull_WhenCredentialsInvalid()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var person = new Person { Dni = "555", Email = "auth@example.com", Password = "password123", IsActive = true };
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        var repository = new PersonRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.AuthenticateAsync("auth@example.com", "wrongpassword");

        // Assert
        Assert.Null(result);
    }
}
