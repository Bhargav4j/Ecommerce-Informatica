using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Categories;

namespace EcommerceInformatica.Web.Tests.Pages.Categories;

public class IndexModelTests
{
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<ILogger<IndexModel>> _mockLogger;
    private readonly IndexModel _pageModel;

    public IndexModelTests()
    {
        _mockCategoryService = new Mock<CategoryService>();
        _mockLogger = new Mock<ILogger<IndexModel>>();
        _pageModel = new IndexModel(_mockCategoryService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithAllCategories_WhenNoSearchString()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Category 1", Description = "Description 1", IsActive = true },
            new CategoryDto { Id = 2, Name = "Category 2", Description = "Description 2", IsActive = true }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(2, _pageModel.Categories.Count());
        _mockCategoryService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredCategories_WhenSearchStringMatchesName()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true },
            new CategoryDto { Id = 2, Name = "Clothing", Description = "Apparel items", IsActive = true },
            new CategoryDto { Id = 3, Name = "Books", Description = "Reading materials", IsActive = true }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _pageModel.SearchString = "Electronics";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Categories);
        Assert.Equal("Electronics", _pageModel.Categories.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredCategories_WhenSearchStringMatchesDescription()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true },
            new CategoryDto { Id = 2, Name = "Clothing", Description = "Apparel items", IsActive = true },
            new CategoryDto { Id = 3, Name = "Books", Description = "Reading materials", IsActive = true }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _pageModel.SearchString = "devices";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Categories);
        Assert.Equal("Electronics", _pageModel.Categories.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_SearchIsCaseInsensitive()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true },
            new CategoryDto { Id = 2, Name = "Clothing", Description = "Apparel items", IsActive = true }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _pageModel.SearchString = "electronics";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Categories);
        Assert.Equal("Electronics", _pageModel.Categories.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsEmptyList_WhenNoCategoriesMatchSearch()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true },
            new CategoryDto { Id = 2, Name = "Clothing", Description = "Apparel items", IsActive = true }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _pageModel.SearchString = "NonExistentCategory";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Categories);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_ReturnsPageWithEmptyList()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Categories);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnGetAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        var exception = new Exception("Database error");
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(exception);

        // Act
        await _pageModel.OnGetAsync();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WhenCategoryServiceReturnsEmptyList()
    {
        // Arrange
        var categories = new List<CategoryDto>();
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Categories);
    }

    [Fact]
    public void SearchString_Property_CanBeSet()
    {
        // Arrange
        var searchString = "TestSearch";

        // Act
        _pageModel.SearchString = searchString;

        // Assert
        Assert.Equal(searchString, _pageModel.SearchString);
    }

    [Fact]
    public void Categories_Property_InitializedAsEmptyList()
    {
        // Arrange & Act
        var pageModel = new IndexModel(_mockCategoryService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(pageModel.Categories);
        Assert.Empty(pageModel.Categories);
    }

    [Fact]
    public async Task OnGetAsync_IncludesInactiveCategories_InResults()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Active Category", Description = "Active", IsActive = true },
            new CategoryDto { Id = 2, Name = "Inactive Category", Description = "Inactive", IsActive = false }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(2, _pageModel.Categories.Count());
    }
}
