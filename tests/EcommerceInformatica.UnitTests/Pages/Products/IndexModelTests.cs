using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using EcommerceInformatica.Web.Pages.Products;
using EcommerceInformatica.Domain.Interfaces.Services;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.Pages.Products;

public class IndexModelTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<ILogger<IndexModel>> _mockLogger;

    public IndexModelTests()
    {
        _mockProductService = new Mock<IProductService>();
        _mockLogger = new Mock<ILogger<IndexModel>>();
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Act
        var model = new IndexModel(_mockProductService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Products);
        Assert.Empty(model.Products);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadAllProducts_WhenSearchTermIsNull()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Product1" },
            new ProductDto { Id = 2, Name = "Product2" }
        };
        _mockProductService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products.Cast<object>());

        var model = new IndexModel(_mockProductService.Object, _mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        Assert.NotNull(model.Products);
        Assert.Equal(2, model.Products.Count());
        _mockProductService.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldSearchProducts_WhenSearchTermIsProvided()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop" }
        };
        _mockProductService.Setup(s => s.SearchAsync("Lap", It.IsAny<CancellationToken>()))
            .ReturnsAsync(products.Cast<object>());

        var model = new IndexModel(_mockProductService.Object, _mockLogger.Object)
        {
            SearchTerm = "Lap"
        };

        // Act
        await model.OnGetAsync();

        // Assert
        Assert.NotNull(model.Products);
        Assert.Single(model.Products);
        _mockProductService.Verify(s => s.SearchAsync("Lap", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleException()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        var model = new IndexModel(_mockProductService.Object, _mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        Assert.NotNull(model.Products);
        Assert.Empty(model.Products);
    }
}
