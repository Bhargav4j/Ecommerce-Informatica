using Xunit;
using EcommerceApp.Web.Pages.Categories;
using EcommerceApp.Application.Interfaces;
using EcommerceApp.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;

namespace EcommerceApp.Web.Pages.Categories.Tests;

public class DetailsModelTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<ILogger<DetailsModel>> _loggerMock;
    private readonly DetailsModel _model;

    public DetailsModelTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _loggerMock = new Mock<ILogger<DetailsModel>>();
        _model = new DetailsModel(_categoryServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void DetailsModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new DetailsModel(_categoryServiceMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(model);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadCategory_WhenExists()
    {
        // Arrange
        var categoryDto = new CategoryDto { Id = 1, Name = "Electronics", Description = "Test" };
        _categoryServiceMock.Setup(s => s.GetByIdAsync(1, default)).ReturnsAsync(categoryDto);

        // Act
        var result = await _model.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.NotNull(_model.Category);
        Assert.Equal(1, _model.Category.Id);
        Assert.Equal("Electronics", _model.Category.Name);
        _categoryServiceMock.Verify(s => s.GetByIdAsync(1, default), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldReturnNotFound_WhenCategoryNotExists()
    {
        // Arrange
        _categoryServiceMock.Setup(s => s.GetByIdAsync(999, default)).ReturnsAsync((CategoryDto?)null);

        // Act
        var result = await _model.OnGetAsync(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ShouldReturnNotFound_WhenExceptionOccurs()
    {
        // Arrange
        _categoryServiceMock.Setup(s => s.GetByIdAsync(1, default)).ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _model.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
