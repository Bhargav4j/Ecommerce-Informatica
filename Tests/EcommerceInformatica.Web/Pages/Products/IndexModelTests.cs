using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Products;

namespace EcommerceInformatica.Web.Tests.Pages.Products;

public class IndexModelTests
{
    private readonly Mock<ProductService> _mockProductService;
    private readonly Mock<ILogger<IndexModel>> _mockLogger;
    private readonly IndexModel _pageModel;

    public IndexModelTests()
    {
        _mockProductService = new Mock<ProductService>();
        _mockLogger = new Mock<ILogger<IndexModel>>();
        _pageModel = new IndexModel(_mockProductService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithAllProducts_WhenNoSearchString()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Product 1", Description = "Description 1", UnitPrice = 10.99m, Stock = 100 },
            new ProductDto { Id = 2, Name = "Product 2", Description = "Description 2", UnitPrice = 20.99m, Stock = 50 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(2, _pageModel.Products.Count());
        _mockProductService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredProducts_WhenSearchStringMatchesName()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming laptop", UnitPrice = 999.99m, Stock = 10 },
            new ProductDto { Id = 2, Name = "Mouse", Description = "Wireless mouse", UnitPrice = 29.99m, Stock = 50 },
            new ProductDto { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", UnitPrice = 79.99m, Stock = 30 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
        _pageModel.SearchString = "Laptop";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Products);
        Assert.Equal("Laptop", _pageModel.Products.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredProducts_WhenSearchStringMatchesDescription()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming laptop", UnitPrice = 999.99m, Stock = 10 },
            new ProductDto { Id = 2, Name = "Mouse", Description = "Wireless mouse", UnitPrice = 29.99m, Stock = 50 },
            new ProductDto { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", UnitPrice = 79.99m, Stock = 30 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
        _pageModel.SearchString = "Gaming";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Products);
        Assert.Equal("Laptop", _pageModel.Products.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredProducts_WhenSearchStringMatchesCategoryName()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming laptop", CategoryName = "Electronics", UnitPrice = 999.99m, Stock = 10 },
            new ProductDto { Id = 2, Name = "Shirt", Description = "Cotton shirt", CategoryName = "Clothing", UnitPrice = 29.99m, Stock = 50 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
        _pageModel.SearchString = "Electronics";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Products);
        Assert.Equal("Laptop", _pageModel.Products.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredProducts_WhenSearchStringMatchesBrandName()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming laptop", BrandName = "Dell", UnitPrice = 999.99m, Stock = 10 },
            new ProductDto { Id = 2, Name = "Mouse", Description = "Wireless mouse", BrandName = "Logitech", UnitPrice = 29.99m, Stock = 50 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
        _pageModel.SearchString = "Dell";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Products);
        Assert.Equal("Laptop", _pageModel.Products.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_SearchIsCaseInsensitive()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming laptop", UnitPrice = 999.99m, Stock = 10 },
            new ProductDto { Id = 2, Name = "Mouse", Description = "Wireless mouse", UnitPrice = 29.99m, Stock = 50 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
        _pageModel.SearchString = "laptop";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Products);
        Assert.Equal("Laptop", _pageModel.Products.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsEmptyList_WhenNoProductsMatchSearch()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming laptop", UnitPrice = 999.99m, Stock = 10 },
            new ProductDto { Id = 2, Name = "Mouse", Description = "Wireless mouse", UnitPrice = 29.99m, Stock = 50 }
        };
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
        _pageModel.SearchString = "NonExistentProduct";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Products);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_ReturnsPageWithEmptyList()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Products);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnGetAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        var exception = new Exception("Database error");
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(exception);

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
    public async Task OnGetAsync_ReturnsPageResult_WhenProductServiceReturnsEmptyList()
    {
        // Arrange
        var products = new List<ProductDto>();
        _mockProductService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Products);
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
    public void Products_Property_InitializedAsEmptyList()
    {
        // Arrange & Act
        var pageModel = new IndexModel(_mockProductService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(pageModel.Products);
        Assert.Empty(pageModel.Products);
    }
}
