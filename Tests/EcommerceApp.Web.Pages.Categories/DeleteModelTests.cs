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

public class DeleteModelTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<ILogger<DeleteModel>> _loggerMock;
    private readonly DeleteModel _model;

    public DeleteModelTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _loggerMock = new Mock<ILogger<DeleteModel>>();
        _model = new DeleteModel(_categoryServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void DeleteModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new DeleteModel(_categoryServiceMock.Object, _loggerMock.Object);

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
    public async Task OnPostAsync_ShouldReturnNotFound_WhenCategoryIsNull()
    {
        // Arrange
        _model.Category = null;

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ShouldDeleteCategory_WhenCategoryExists()
    {
        // Arrange
        _model.Category = new CategoryDto { Id = 1, Name = "Electronics" };
        _categoryServiceMock.Setup(s => s.DeleteAsync(1, default)).Returns(Task.CompletedTask);

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        _categoryServiceMock.Verify(s => s.DeleteAsync(1, default), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_ShouldReturnPage_WhenExceptionOccurs()
    {
        // Arrange
        _model.Category = new CategoryDto { Id = 1, Name = "Electronics" };
        _categoryServiceMock.Setup(s => s.DeleteAsync(1, default)).ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(_model.ModelState.IsValid);
    }
}
