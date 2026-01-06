using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Repositories;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Infrastructure.Repositories.Tests;

/// <summary>
/// Test class for CategoryRepository
/// </summary>
public class CategoryRepositoryTests
{
    private readonly Mock<ILogger<CategoryRepository>> _loggerMock;

    public CategoryRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CategoryRepository>>();
    }

    private DbContextOptions<EcommerceDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActiveCategories()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        context.Categories.Add(new Category { Name = "Category 1", IsActive = true, CreatedBy = "Test" });
        context.Categories.Add(new Category { Name = "Category 2", IsActive = true, CreatedBy = "Test" });
        context.Categories.Add(new Category { Name = "Category 3", IsActive = false, CreatedBy = "Test" });
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var categories = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, categories.Count());
        Assert.All(categories, c => Assert.True(c.IsActive));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var category = new Category { Name = "Test Category", IsActive = true, CreatedBy = "Test" };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(category.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Category", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);
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
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _loggerMock.Object);

        var category = new Category
        {
            Name = "New Category",
            IsActive = true,
            CreatedBy = "Test"
        };

        // Act
        var result = await repository.AddAsync(category);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Single(context.Categories);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var category = new Category { Name = "Original Name", IsActive = true, CreatedBy = "Test" };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);
        category.Name = "Updated Name";

        // Act
        await repository.UpdateAsync(category);

        // Assert
        var updated = await context.Categories.FindAsync(category.Id);
        Assert.Equal("Updated Name", updated?.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteCategory()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var category = new Category { Name = "Test Category", IsActive = true, CreatedBy = "Test" };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        await repository.DeleteAsync(category.Id);

        // Assert
        var deleted = await context.Categories.FindAsync(category.Id);
        Assert.NotNull(deleted);
        Assert.False(deleted.IsActive);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenCategoryExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var category = new Category { Name = "Test Category", IsActive = true, CreatedBy = "Test" };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var exists = await repository.ExistsAsync(category.Id);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenCategoryNotExists()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var exists = await repository.ExistsAsync(999);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingCategories()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        context.Categories.Add(new Category { Name = "Electronics", IsActive = true, CreatedBy = "Test" });
        context.Categories.Add(new Category { Name = "Electronic Devices", IsActive = true, CreatedBy = "Test" });
        context.Categories.Add(new Category { Name = "Furniture", IsActive = true, CreatedBy = "Test" });
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context, _loggerMock.Object);

        // Act
        var results = await repository.SearchAsync("Electronic");

        // Assert
        Assert.Equal(2, results.Count());
        Assert.All(results, c => Assert.Contains("Electronic", c.Name));
    }
}
