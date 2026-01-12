using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Brands;

namespace EcommerceInformatica.Web.Tests.Pages.Brands;

public class DeleteModelTests
{
    private readonly Mock<BrandService> _mockBrandService;
    private readonly Mock<ILogger<DeleteModel>> _mockLogger;
    private readonly DeleteModel _pageModel;

    public DeleteModelTests()
    {
        _mockBrandService = new Mock<BrandService>();
        _mockLogger = new Mock<ILogger<DeleteModel>>();
        _pageModel = new DeleteModel(_mockBrandService.Object, _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _pageModel.OnGetAsync(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockBrandService.Verify(s => s.GetByIdAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenBrandDoesNotExist()
    {
        // Arrange
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync((BrandDto)null);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.Null(_pageModel.Brand);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WhenBrandExists()
    {
        // Arrange
        var brandDto = new BrandDto
        {
            Id = 1,
            Name = "Test Brand",
            Description = "Test Description",
            IsActive = true
        };
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(brandDto);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.NotNull(_pageModel.Brand);
        Assert.Equal(1, _pageModel.Brand.Id);
        Assert.Equal("Test Brand", _pageModel.Brand.Name);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

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
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(exception);

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
    public async Task OnPostAsync_ReturnsNotFound_WhenBrandIsNull()
    {
        // Arrange
        _pageModel.Brand = null;

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockBrandService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsNotFound_WhenBrandIdIsZero()
    {
        // Arrange
        _pageModel.Brand = new BrandDto { Id = 0 };

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockBrandService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_DeletesBrand_RedirectsToIndex()
    {
        // Arrange
        _pageModel.Brand = new BrandDto { Id = 1, Name = "Test Brand" };
        _mockBrandService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Brand deleted successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockBrandService.Verify(s => s.DeleteAsync(1, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_CallsDeleteAsync_WithCorrectId()
    {
        // Arrange
        _pageModel.Brand = new BrandDto { Id = 5, Name = "Brand" };
        _mockBrandService.Setup(s => s.DeleteAsync(5, CancellationToken.None)).ReturnsAsync(true);

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        _mockBrandService.Verify(s => s.DeleteAsync(5, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _pageModel.Brand = new BrandDto { Id = 1, Name = "Brand" };
        _mockBrandService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("An error occurred while deleting the brand.", _pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        _pageModel.Brand = new BrandDto { Id = 1, Name = "Brand" };
        var exception = new Exception("Database error");
        _mockBrandService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ThrowsAsync(exception);

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
    public void Brand_Property_IsNullableByDefault()
    {
        // Arrange & Act
        var pageModel = new DeleteModel(_mockBrandService.Object, _mockLogger.Object);

        // Assert
        Assert.Null(pageModel.Brand);
    }
}
