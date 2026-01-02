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

public class CategoryRepositoryTests
{
    private readonly Mock<ILogger<CategoryRepository>> _loggerMock;
    private readonly DbContextOptions<EcommerceDbContext> _options;

    public CategoryRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CategoryRepository>>();
        _options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActiveCategories()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        context.Categories.Add(new Category { Id = 1, Name = "Active", IsActive = true });
        context.Categories.Add(new Category { Id = 2, Name = "Inactive", IsActive = false });
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Electronics", IsActive = true };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Electronics", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCategory()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new CategoryRepository(context, _loggerMock.Object);
        var category = new Category { Name = "Books", IsActive = true };

        // Act
        var result = await repository.AddAsync(category);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Books", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Old Name", IsActive = true };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);
        category.Name = "New Name";

        // Act
        await repository.UpdateAsync(category);

        // Assert
        var updated = await context.Categories.FindAsync(1);
        Assert.Equal("New Name", updated?.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSetIsActiveToFalse()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "ToDelete", IsActive = true };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        await repository.DeleteAsync(1);

        // Assert
        var deleted = await context.Categories.FindAsync(1);
        Assert.False(deleted?.IsActive);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var category = new Category { Id = 1, Name = "Exists", IsActive = true };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingCategories()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        context.Categories.Add(new Category { Id = 1, Name = "Electronics", IsActive = true });
        context.Categories.Add(new Category { Id = 2, Name = "Books", IsActive = true });
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.SearchAsync("Elec");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnEmpty_WhenNoMatch()
    {
        // Arrange
        using var context = new EcommerceDbContext(_options);
        context.Categories.Add(new Category { Id = 1, Name = "Electronics", IsActive = true });
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.SearchAsync("XYZ");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
