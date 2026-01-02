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

public class EditModelTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<ILogger<EditModel>> _loggerMock;
    private readonly EditModel _model;

    public EditModelTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _loggerMock = new Mock<ILogger<EditModel>>();
        _model = new EditModel(_categoryServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void EditModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new EditModel(_categoryServiceMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(model);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadCategory_WhenExists()
    {
        // Arrange
        var categoryDto = new CategoryDto { Id = 1, Name = "Electronics", Description = "Test", IsActive = true };
        _categoryServiceMock.Setup(s => s.GetByIdAsync(1, default)).ReturnsAsync(categoryDto);

        // Act
        var result = await _model.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(1, _model.Id);
        Assert.Equal("Electronics", _model.Name);
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
    public async Task OnPostAsync_ShouldReturnPage_WhenModelStateInvalid()
    {
        // Arrange
        _model.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ShouldUpdateCategory_WhenModelStateValid()
    {
        // Arrange
        _model.Id = 1;
        _model.Name = "Updated Category";
        _model.Description = "Updated Description";
        _model.IsActive = true;

        _categoryServiceMock.Setup(s => s.UpdateAsync(1, It.IsAny<CategoryUpdateDto>(), default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        _categoryServiceMock.Verify(s => s.UpdateAsync(1, It.IsAny<CategoryUpdateDto>(), default), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_ShouldReturnPage_WhenExceptionOccurs()
    {
        // Arrange
        _model.Id = 1;
        _model.Name = "Test";
        _categoryServiceMock.Setup(s => s.UpdateAsync(1, It.IsAny<CategoryUpdateDto>(), default))
            .ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(_model.ModelState.IsValid);
    }
}
