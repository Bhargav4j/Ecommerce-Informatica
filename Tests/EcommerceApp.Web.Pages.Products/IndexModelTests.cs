using Xunit;
using EcommerceApp.Web.Pages.Products;
using EcommerceApp.Application.Interfaces;
using EcommerceApp.Application.DTOs;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EcommerceApp.Web.Pages.Products.Tests;

public class IndexModelTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<ILogger<IndexModel>> _loggerMock;
    private readonly IndexModel _model;

    public IndexModelTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _loggerMock = new Mock<ILogger<IndexModel>>();
        _model = new IndexModel(_productServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void IndexModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new IndexModel(_productServiceMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Products);
        Assert.Empty(model.Products);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadProducts()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop" },
            new ProductDto { Id = 2, Name = "Mouse" }
        };

        _productServiceMock.Setup(s => s.GetAllAsync(default)).ReturnsAsync(products);

        // Act
        await _model.OnGetAsync();

        // Assert
        Assert.NotNull(_model.Products);
        Assert.Equal(2, ((List<ProductDto>)_model.Products).Count);
        _productServiceMock.Verify(s => s.GetAllAsync(default), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleEmptyProducts()
    {
        // Arrange
        var emptyProducts = new List<ProductDto>();
        _productServiceMock.Setup(s => s.GetAllAsync(default)).ReturnsAsync(emptyProducts);

        // Act
        await _model.OnGetAsync();

        // Assert
        Assert.NotNull(_model.Products);
        Assert.Empty(_model.Products);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleException()
    {
        // Arrange
        _productServiceMock.Setup(s => s.GetAllAsync(default)).ThrowsAsync(new System.Exception("Test exception"));

        // Act
        await _model.OnGetAsync();

        // Assert
        Assert.NotNull(_model.Products);
        Assert.Empty(_model.Products);
    }
}
