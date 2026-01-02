using Xunit;
using EcommerceApp.Web.Pages.Categories;
using EcommerceApp.Application.Interfaces;
using EcommerceApp.Application.DTOs;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EcommerceApp.Web.Pages.Categories.Tests;

public class IndexModelTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<ILogger<IndexModel>> _loggerMock;
    private readonly IndexModel _model;

    public IndexModelTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _loggerMock = new Mock<ILogger<IndexModel>>();
        _model = new IndexModel(_categoryServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void IndexModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new IndexModel(_categoryServiceMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Categories);
        Assert.Empty(model.Categories);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadCategories()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Electronics" },
            new CategoryDto { Id = 2, Name = "Books" }
        };

        _categoryServiceMock.Setup(s => s.GetAllAsync(default)).ReturnsAsync(categories);

        // Act
        await _model.OnGetAsync();

        // Assert
        Assert.NotNull(_model.Categories);
        Assert.Equal(2, ((List<CategoryDto>)_model.Categories).Count);
        _categoryServiceMock.Verify(s => s.GetAllAsync(default), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleEmptyCategories()
    {
        // Arrange
        var emptyCategories = new List<CategoryDto>();
        _categoryServiceMock.Setup(s => s.GetAllAsync(default)).ReturnsAsync(emptyCategories);

        // Act
        await _model.OnGetAsync();

        // Assert
        Assert.NotNull(_model.Categories);
        Assert.Empty(_model.Categories);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleException()
    {
        // Arrange
        _categoryServiceMock.Setup(s => s.GetAllAsync(default)).ThrowsAsync(new System.Exception("Test exception"));

        // Act
        await _model.OnGetAsync();

        // Assert
        Assert.NotNull(_model.Categories);
        Assert.Empty(_model.Categories);
    }
}
