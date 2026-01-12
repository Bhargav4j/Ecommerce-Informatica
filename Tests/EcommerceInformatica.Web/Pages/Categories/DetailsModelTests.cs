using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Categories;

namespace EcommerceInformatica.Web.Tests.Pages.Categories;

public class DetailsModelTests
{
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<ILogger<DetailsModel>> _mockLogger;
    private readonly DetailsModel _pageModel;

    public DetailsModelTests()
    {
        _mockCategoryService = new Mock<CategoryService>();
        _mockLogger = new Mock<ILogger<DetailsModel>>();
        _pageModel = new DetailsModel(_mockCategoryService.Object, _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _pageModel.OnGetAsync(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockCategoryService.Verify(s => s.GetByIdAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync((CategoryDto)null);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.Null(_pageModel.Category);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WhenCategoryExists()
    {
        // Arrange
        var categoryDto = new CategoryDto
        {
            Id = 1,
            Name = "Test Category",
            Description = "Test Description",
            IsActive = true
        };
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(categoryDto);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.NotNull(_pageModel.Category);
        Assert.Equal(1, _pageModel.Category.Id);
        Assert.Equal("Test Category", _pageModel.Category.Name);
        Assert.Equal("Test Description", _pageModel.Category.Description);
        Assert.True(_pageModel.Category.IsActive);
    }

    [Fact]
    public async Task OnGetAsync_CallsGetByIdAsync_WithCorrectId()
    {
        // Arrange
        var categoryDto = new CategoryDto { Id = 5, Name = "Category" };
        _mockCategoryService.Setup(s => s.GetByIdAsync(5, CancellationToken.None)).ReturnsAsync(categoryDto);

        // Act
        await _pageModel.OnGetAsync(5);

        // Assert
        _mockCategoryService.Verify(s => s.GetByIdAsync(5, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnGetAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        var exception = new Exception("Database error");
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(exception);

        // Act
        await _pageModel.OnGetAsync(1);

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
    public async Task OnGetAsync_SetsErrorMessage_WhenExceptionOccurs()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        // Act
        await _pageModel.OnGetAsync(1);

        // Assert
        Assert.Equal("An error occurred while retrieving the category.", _pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public void Category_Property_IsNullableByDefault()
    {
        // Arrange & Act
        var pageModel = new DetailsModel(_mockCategoryService.Object, _mockLogger.Object);

        // Assert
        Assert.Null(pageModel.Category);
    }
}
