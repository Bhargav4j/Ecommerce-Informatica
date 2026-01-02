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
using System.Linq;

namespace EcommerceApp.Application.Services.Tests;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CategoryService>> _loggerMock;
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _repositoryMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CategoryService>>();
        _service = new CategoryService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnCategoryDtos()
    {
        // Arrange
        var categories = new List<Category> { new Category { Id = 1, Name = "Test" } };
        var categoryDtos = new List<CategoryDto> { new CategoryDto { Id = 1, Name = "Test" } };

        _repositoryMock.Setup(r => r.GetAllAsync(default)).ReturnsAsync(categories);
        _mapperMock.Setup(m => m.Map<IEnumerable<CategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _repositoryMock.Verify(r => r.GetAllAsync(default), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategoryDto_WhenExists()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Test" };
        var categoryDto = new CategoryDto { Id = 1, Name = "Test" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(category);
        _mapperMock.Setup(m => m.Map<CategoryDto>(category)).Returns(categoryDto);

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
        _repositoryMock.Setup(r => r.GetByIdAsync(999, default)).ReturnsAsync((Category?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(r => r.GetByIdAsync(999, default), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedCategoryDto()
    {
        // Arrange
        var createDto = new CategoryCreateDto { Name = "New Category" };
        var category = new Category { Name = "New Category" };
        var createdCategory = new Category { Id = 1, Name = "New Category" };
        var categoryDto = new CategoryDto { Id = 1, Name = "New Category" };

        _mapperMock.Setup(m => m.Map<Category>(createDto)).Returns(category);
        _repositoryMock.Setup(r => r.AddAsync(category, default)).ReturnsAsync(createdCategory);
        _mapperMock.Setup(m => m.Map<CategoryDto>(createdCategory)).Returns(categoryDto);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Category", result.Name);
        _repositoryMock.Verify(r => r.AddAsync(category, default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory_WhenExists()
    {
        // Arrange
        var updateDto = new CategoryUpdateDto { Name = "Updated" };
        var existingCategory = new Category { Id = 1, Name = "Old" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(existingCategory);
        _mapperMock.Setup(m => m.Map(updateDto, existingCategory)).Returns(existingCategory);
        _repositoryMock.Setup(r => r.UpdateAsync(existingCategory, default)).Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(1, updateDto);

        // Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(1, default), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(existingCategory, default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenNotExists()
    {
        // Arrange
        var updateDto = new CategoryUpdateDto { Name = "Updated" };
        _repositoryMock.Setup(r => r.GetByIdAsync(999, default)).ReturnsAsync((Category?)null);

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
    public async Task SearchAsync_ShouldReturnMatchingCategories()
    {
        // Arrange
        var categories = new List<Category> { new Category { Id = 1, Name = "Electronics" } };
        var categoryDtos = new List<CategoryDto> { new CategoryDto { Id = 1, Name = "Electronics" } };

        _repositoryMock.Setup(r => r.SearchAsync("Elec", default)).ReturnsAsync(categories);
        _mapperMock.Setup(m => m.Map<IEnumerable<CategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _service.SearchAsync("Elec");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _repositoryMock.Verify(r => r.SearchAsync("Elec", default), Times.Once);
    }
}
