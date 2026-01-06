using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;

namespace EcommerceInformatica.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<ProductService>> _mockLogger;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<ProductService>>();
        _service = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Product1" } };
        var productDtos = new List<ProductDto> { new ProductDto { Id = 1, Name = "Product1" } };
        _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(products);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<object>>(result);
        _mockRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1" };
        var productDto = new ProductDto { Id = 1, Name = "Product1" };
        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
        _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateProduct()
    {
        // Arrange
        var createDto = new ProductCreateDto { Name = "NewProduct", UnitPrice = 100, Stock = 10, CategoryId = 1, BrandId = 1, SupplierId = 1 };
        var product = new Product { Id = 1, Name = "NewProduct" };
        var productDto = new ProductDto { Id = 1, Name = "NewProduct" };
        _mockMapper.Setup(m => m.Map<Product>(createDto)).Returns(product);
        _mockRepository.Setup(r => r.AddAsync(product, It.IsAny<CancellationToken>())).ReturnsAsync(product);
        _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(r => r.AddAsync(product, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct_WhenProductExists()
    {
        // Arrange
        var updateDto = new ProductUpdateDto { Name = "UpdatedProduct", UnitPrice = 150, Stock = 20, CategoryId = 1, BrandId = 1, SupplierId = 1 };
        var existingProduct = new Product { Id = 1, Name = "Product1" };
        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingProduct);
        _mockMapper.Setup(m => m.Map(updateDto, existingProduct)).Returns(existingProduct);

        // Act
        await _service.UpdateAsync(1, updateDto);

        // Assert
        _mockRepository.Verify(r => r.UpdateAsync(existingProduct, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExist()
    {
        // Arrange
        var updateDto = new ProductUpdateDto { Name = "UpdatedProduct" };
        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(1, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProduct()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _mockRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingProducts()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "SearchProduct" } };
        var productDtos = new List<ProductDto> { new ProductDto { Id = 1, Name = "SearchProduct" } };
        _mockRepository.Setup(r => r.SearchAsync("Search", It.IsAny<CancellationToken>())).ReturnsAsync(products);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

        // Act
        var result = await _service.SearchAsync("Search");

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<object>>(result);
        _mockRepository.Verify(r => r.SearchAsync("Search", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldReturnProductsInCategory()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Product1", CategoryId = 1 } };
        var productDtos = new List<ProductDto> { new ProductDto { Id = 1, Name = "Product1", CategoryId = 1 } };
        _mockRepository.Setup(r => r.GetByCategoryAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(products);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

        // Act
        var result = await _service.GetByCategoryAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<object>>(result);
        _mockRepository.Verify(r => r.GetByCategoryAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }
}
