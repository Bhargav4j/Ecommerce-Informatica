using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Repositories;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Repositories;

public class CategoryRepositoryTests
{
    private readonly Mock<ILogger<CategoryRepository>> _mockLogger;

    public CategoryRepositoryTests()
    {
        _mockLogger = new Mock<ILogger<CategoryRepository>>();
    }

    private DbContextOptions<EcommerceDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActiveCategories()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        context.Categories.Add(new Category { Id = 1, Name = "Category1", IsActive = true });
        context.Categories.Add(new Category { Id = 2, Name = "Category2", IsActive = false });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Category1", result.First().Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenExists()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        context.Categories.Add(new Category { Id = 1, Name = "Category1", IsActive = true });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Category1", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenDoesNotExist()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCategory()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);
        var category = new Category { Name = "NewCategory", CreatedBy = "Admin" };

        // Act
        var result = await repository.AddAsync(category);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("NewCategory", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        var category = new Category { Id = 1, Name = "Category1", IsActive = true };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        category.Name = "UpdatedCategory";

        // Act
        await repository.UpdateAsync(category);
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("UpdatedCategory", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteCategory()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        var category = new Category { Id = 1, Name = "Category1", IsActive = true };
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(1);
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenCategoryExists()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        context.Categories.Add(new Category { Id = 1, Name = "Category1", IsActive = true });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingCategories()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var repository = new CategoryRepository(context, _mockLogger.Object);

        context.Categories.Add(new Category { Id = 1, Name = "Electronics", IsActive = true });
        context.Categories.Add(new Category { Id = 2, Name = "Books", IsActive = true });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.SearchAsync("Elec");

        // Assert
        Assert.Single(result);
        Assert.Equal("Electronics", result.First().Name);
    }
}
