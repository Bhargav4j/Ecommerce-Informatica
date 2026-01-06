using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using EcommerceInformatica.Web.Pages.Categories;
using EcommerceInformatica.Domain.Interfaces.Services;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.Pages.Categories;

public class IndexModelTests
{
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly Mock<ILogger<IndexModel>> _mockLogger;

    public IndexModelTests()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _mockLogger = new Mock<ILogger<IndexModel>>();
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Act
        var model = new IndexModel(_mockCategoryService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Categories);
        Assert.Empty(model.Categories);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadCategories()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Category1" },
            new CategoryDto { Id = 2, Name = "Category2" }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(categories.Cast<object>());

        var model = new IndexModel(_mockCategoryService.Object, _mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        Assert.NotNull(model.Categories);
        Assert.Equal(2, model.Categories.Count());
        _mockCategoryService.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleException()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        var model = new IndexModel(_mockCategoryService.Object, _mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        Assert.NotNull(model.Categories);
        Assert.Empty(model.Categories);
    }
}
