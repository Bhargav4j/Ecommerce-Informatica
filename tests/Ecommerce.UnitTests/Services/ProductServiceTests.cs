using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Ecommerce.Application.Services;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;

namespace Ecommerce.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<ProductService>> _loggerMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<ProductService>>();
        _service = new ProductService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Products()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Description = "Product 1" },
            new Product { Id = 2, Description = "Product 2" }
        };

        var productDtos = new List<ProductDto>
        {
            new ProductDto { Id = 1, Description = "Product 1" },
            new ProductDto { Id = 2, Description = "Product 2" }
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products))
            .Returns(productDtos);

        var result = await _service.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(productDtos);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Product_When_Exists()
    {
        var product = new Product { Id = 1, Description = "Test Product" };
        var productDto = new ProductDto { Id = 1, Description = "Test Product" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        _mapperMock.Setup(m => m.Map<ProductDto>(product))
            .Returns(productDto);

        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(productDto);
        _repositoryMock.Verify(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }
}
