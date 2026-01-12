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

public class DetailsModelTests
{
    private readonly Mock<ProductService> _mockProductService;
    private readonly Mock<ILogger<DetailsModel>> _mockLogger;
    private readonly DetailsModel _pageModel;

    public DetailsModelTests()
    {
        _mockProductService = new Mock<ProductService>();
        _mockLogger = new Mock<ILogger<DetailsModel>>();
        _pageModel = new DetailsModel(_mockProductService.Object, _mockLogger.Object);
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
        Assert.Equal("Test Description", _pageModel.Product.Description);
        Assert.Equal(99.99m, _pageModel.Product.UnitPrice);
        Assert.Equal(50, _pageModel.Product.Stock);
    }

    [Fact]
    public async Task OnGetAsync_CallsGetByIdAsync_WithCorrectId()
    {
        // Arrange
        var productDto = new ProductDto { Id = 5, Name = "Product" };
        _mockProductService.Setup(s => s.GetByIdAsync(5, CancellationToken.None)).ReturnsAsync(productDto);

        // Act
        await _pageModel.OnGetAsync(5);

        // Assert
        _mockProductService.Verify(s => s.GetByIdAsync(5, CancellationToken.None), Times.Once);
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
    public async Task OnGetAsync_SetsErrorMessage_WhenExceptionOccurs()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        // Act
        await _pageModel.OnGetAsync(1);

        // Assert
        Assert.Equal("An error occurred while retrieving the product.", _pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public void Product_Property_IsNullableByDefault()
    {
        // Arrange & Act
        var pageModel = new DetailsModel(_mockProductService.Object, _mockLogger.Object);

        // Assert
        Assert.Null(pageModel.Product);
    }

    [Fact]
    public async Task OnGetAsync_PopulatesAllProductProperties()
    {
        // Arrange
        var productDto = new ProductDto
        {
            Id = 10,
            Name = "Full Product",
            Description = "Complete Description",
            UnitPrice = 149.99m,
            Stock = 75,
            IsActive = true,
            CategoryId = 2,
            CategoryName = "Electronics",
            BrandId = 3,
            BrandName = "Samsung",
            SupplierId = 4,
            SupplierName = "Tech Supplier"
        };
        _mockProductService.Setup(s => s.GetByIdAsync(10, CancellationToken.None)).ReturnsAsync(productDto);

        // Act
        var result = await _pageModel.OnGetAsync(10);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.NotNull(_pageModel.Product);
        Assert.Equal(10, _pageModel.Product.Id);
        Assert.Equal("Full Product", _pageModel.Product.Name);
        Assert.Equal("Complete Description", _pageModel.Product.Description);
        Assert.Equal(149.99m, _pageModel.Product.UnitPrice);
        Assert.Equal(75, _pageModel.Product.Stock);
        Assert.True(_pageModel.Product.IsActive);
        Assert.Equal(2, _pageModel.Product.CategoryId);
        Assert.Equal("Electronics", _pageModel.Product.CategoryName);
        Assert.Equal(3, _pageModel.Product.BrandId);
        Assert.Equal("Samsung", _pageModel.Product.BrandName);
        Assert.Equal(4, _pageModel.Product.SupplierId);
        Assert.Equal("Tech Supplier", _pageModel.Product.SupplierName);
    }
}
