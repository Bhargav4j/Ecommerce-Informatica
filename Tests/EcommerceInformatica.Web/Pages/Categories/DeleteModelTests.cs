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

public class DeleteModelTests
{
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<ILogger<DeleteModel>> _mockLogger;
    private readonly DeleteModel _pageModel;

    public DeleteModelTests()
    {
        _mockCategoryService = new Mock<CategoryService>();
        _mockLogger = new Mock<ILogger<DeleteModel>>();
        _pageModel = new DeleteModel(_mockCategoryService.Object, _mockLogger.Object);
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
    public async Task OnPostAsync_ReturnsNotFound_WhenCategoryIsNull()
    {
        // Arrange
        _pageModel.Category = null;

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockCategoryService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsNotFound_WhenCategoryIdIsZero()
    {
        // Arrange
        _pageModel.Category = new CategoryDto { Id = 0 };

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockCategoryService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_DeletesCategory_RedirectsToIndex()
    {
        // Arrange
        _pageModel.Category = new CategoryDto { Id = 1, Name = "Test Category" };
        _mockCategoryService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Category deleted successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockCategoryService.Verify(s => s.DeleteAsync(1, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_CallsDeleteAsync_WithCorrectId()
    {
        // Arrange
        _pageModel.Category = new CategoryDto { Id = 5, Name = "Category" };
        _mockCategoryService.Setup(s => s.DeleteAsync(5, CancellationToken.None)).ReturnsAsync(true);

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        _mockCategoryService.Verify(s => s.DeleteAsync(5, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _pageModel.Category = new CategoryDto { Id = 1, Name = "Category" };
        _mockCategoryService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("An error occurred while deleting the category.", _pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        _pageModel.Category = new CategoryDto { Id = 1, Name = "Category" };
        var exception = new Exception("Database error");
        _mockCategoryService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ThrowsAsync(exception);

        // Act
        await _pageModel.OnPostAsync();

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
    public void Category_Property_IsNullableByDefault()
    {
        // Arrange & Act
        var pageModel = new DeleteModel(_mockCategoryService.Object, _mockLogger.Object);

        // Assert
        Assert.Null(pageModel.Category);
    }
}
