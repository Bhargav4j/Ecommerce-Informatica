using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Products;

namespace EcommerceInformatica.Web.Tests.Pages.Products;

public class DeleteModelTests
{
    private readonly Mock<ProductService> _mockProductService;
    private readonly Mock<ILogger<DeleteModel>> _mockLogger;
    private readonly DeleteModel _pageModel;

    public DeleteModelTests()
    {
        _mockProductService = new Mock<ProductService>();
        _mockLogger = new Mock<ILogger<DeleteModel>>();
        _pageModel = new DeleteModel(_mockProductService.Object, _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _pageModel.OnGetAsync(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockProductService.Verify(s => s.GetByIdAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync((ProductDto)null);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.Null(_pageModel.Product);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WhenProductExists()
    {
        // Arrange
        var productDto = new ProductDto
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            UnitPrice = 99.99m,
            Stock = 50,
            IsActive = true
        };
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(productDto);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.NotNull(_pageModel.Product);
        Assert.Equal(1, _pageModel.Product.Id);
        Assert.Equal("Test Product", _pageModel.Product.Name);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

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
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(exception);

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
    public async Task OnPostAsync_ReturnsNotFound_WhenProductIsNull()
    {
        // Arrange
        _pageModel.Product = null;

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockProductService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsNotFound_WhenProductIdIsZero()
    {
        // Arrange
        _pageModel.Product = new ProductDto { Id = 0 };

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockProductService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_DeletesProduct_RedirectsToIndex()
    {
        // Arrange
        _pageModel.Product = new ProductDto { Id = 1, Name = "Test Product" };
        _mockProductService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Product deleted successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockProductService.Verify(s => s.DeleteAsync(1, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_CallsDeleteAsync_WithCorrectId()
    {
        // Arrange
        _pageModel.Product = new ProductDto { Id = 5, Name = "Product" };
        _mockProductService.Setup(s => s.DeleteAsync(5, CancellationToken.None)).ReturnsAsync(true);

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        _mockProductService.Verify(s => s.DeleteAsync(5, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _pageModel.Product = new ProductDto { Id = 1, Name = "Product" };
        _mockProductService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("An error occurred while deleting the product.", _pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        _pageModel.Product = new ProductDto { Id = 1, Name = "Product" };
        var exception = new Exception("Database error");
        _mockProductService.Setup(s => s.DeleteAsync(1, CancellationToken.None)).ThrowsAsync(exception);

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
    public void Product_Property_IsNullableByDefault()
    {
        // Arrange & Act
        var pageModel = new DeleteModel(_mockProductService.Object, _mockLogger.Object);

        // Assert
        Assert.Null(pageModel.Product);
    }

    [Fact]
    public async Task OnPostAsync_DoesNotDeleteProduct_WhenProductIdIsInvalid()
    {
        // Arrange
        _pageModel.Product = new ProductDto { Id = -1 };

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockProductService.Verify(s => s.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }
}
