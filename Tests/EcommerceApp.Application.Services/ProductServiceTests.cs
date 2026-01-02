using Xunit;
using EcommerceApp.Application.Services;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EcommerceApp.Application.Services.Tests;

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
    public async Task GetAllAsync_ShouldReturnProductDtos()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop" } };
        var productDtos = new List<ProductDto> { new ProductDto { Id = 1, Name = "Laptop" } };

        _repositoryMock.Setup(r => r.GetAllAsync(default)).ReturnsAsync(products);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _repositoryMock.Verify(r => r.GetAllAsync(default), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProductDto_WhenExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Mouse" };
        var productDto = new ProductDto { Id = 1, Name = "Mouse" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _repositoryMock.Verify(r => r.GetByIdAsync(1, default), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(999, default)).ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(r => r.GetByIdAsync(999, default), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedProductDto()
    {
        // Arrange
        var createDto = new ProductCreateDto { Name = "Keyboard", Price = 49.99m };
        var product = new Product { Name = "Keyboard", Price = 49.99m };
        var createdProduct = new Product { Id = 1, Name = "Keyboard", Price = 49.99m };
        var productDto = new ProductDto { Id = 1, Name = "Keyboard", Price = 49.99m };

        _mapperMock.Setup(m => m.Map<Product>(createDto)).Returns(product);
        _repositoryMock.Setup(r => r.AddAsync(product, default)).ReturnsAsync(createdProduct);
        _mapperMock.Setup(m => m.Map<ProductDto>(createdProduct)).Returns(productDto);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Keyboard", result.Name);
        _repositoryMock.Verify(r => r.AddAsync(product, default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct_WhenExists()
    {
        // Arrange
        var updateDto = new ProductUpdateDto { Name = "Updated Product", Price = 99.99m };
        var existingProduct = new Product { Id = 1, Name = "Old Product", Price = 50m };

        _repositoryMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(existingProduct);
        _mapperMock.Setup(m => m.Map(updateDto, existingProduct)).Returns(existingProduct);
        _repositoryMock.Setup(r => r.UpdateAsync(existingProduct, default)).Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(1, updateDto);

        // Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(1, default), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(existingProduct, default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenNotExists()
    {
        // Arrange
        var updateDto = new ProductUpdateDto { Name = "Updated" };
        _repositoryMock.Setup(r => r.GetByIdAsync(999, default)).ReturnsAsync((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.UpdateAsync(999, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryDelete()
    {
        // Arrange
        _repositoryMock.Setup(r => r.DeleteAsync(1, default)).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(1, default), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingProducts()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop Dell" } };
        var productDtos = new List<ProductDto> { new ProductDto { Id = 1, Name = "Laptop Dell" } };

        _repositoryMock.Setup(r => r.SearchAsync("Laptop", default)).ReturnsAsync(products);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

        // Act
        var result = await _service.SearchAsync("Laptop");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _repositoryMock.Verify(r => r.SearchAsync("Laptop", default), Times.Once);
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldReturnProductsInCategory()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop", CategoryId = 1 } };
        var productDtos = new List<ProductDto> { new ProductDto { Id = 1, Name = "Laptop", CategoryId = 1 } };

        _repositoryMock.Setup(r => r.GetByCategoryAsync(1, default)).ReturnsAsync(products);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

        // Act
        var result = await _service.GetByCategoryAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _repositoryMock.Verify(r => r.GetByCategoryAsync(1, default), Times.Once);
    }
}
