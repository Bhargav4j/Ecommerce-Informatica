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

public class CreateModelTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<ILogger<CreateModel>> _loggerMock;
    private readonly CreateModel _model;

    public CreateModelTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _loggerMock = new Mock<ILogger<CreateModel>>();
        _model = new CreateModel(_categoryServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void CreateModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new CreateModel(_categoryServiceMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(model);
    }

    [Fact]
    public void OnGet_ShouldExecuteWithoutErrors()
    {
        // Arrange & Act
        _model.OnGet();

        // Assert
        Assert.NotNull(_model);
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
    public async Task OnPostAsync_ShouldCreateCategory_WhenModelStateValid()
    {
        // Arrange
        _model.Name = "Test Category";
        _model.Description = "Test Description";

        _categoryServiceMock.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), default))
            .ReturnsAsync(new CategoryDto { Id = 1, Name = "Test Category" });

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        _categoryServiceMock.Verify(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), default), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_ShouldReturnPage_WhenExceptionOccurs()
    {
        // Arrange
        _model.Name = "Test Category";
        _categoryServiceMock.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), default))
            .ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _model.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(_model.ModelState.IsValid);
    }
}
