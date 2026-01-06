using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Web.Pages.Categories;
using EcommerceInformatica.Domain.Interfaces.Services;
using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.UnitTests.Pages.Categories;

public class CreateModelTests
{
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly Mock<ILogger<CreateModel>> _mockLogger;

    public CreateModelTests()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _mockLogger = new Mock<ILogger<CreateModel>>();
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Act
        var model = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Input);
    }

    [Fact]
    public void OnGet_ShouldExecuteWithoutErrors()
    {
        // Arrange
        var model = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);

        // Act
        model.OnGet();

        // Assert - No exceptions thrown
        Assert.NotNull(model);
    }

    [Fact]
    public async Task OnPostAsync_ShouldReturnPage_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);
        model.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await model.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ShouldCreateCategoryAndRedirect_WhenModelStateIsValid()
    {
        // Arrange
        var model = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);
        model.Input = new CreateModel.InputModel
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        var createdCategory = new CategoryDto { Id = 1, Name = "Test Category" };
        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCategory);

        // Act
        var result = await model.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("./Index", redirectResult.PageName);
        _mockCategoryService.Verify(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_ShouldHandleException()
    {
        // Arrange
        var model = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);
        model.Input = new CreateModel.InputModel
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await model.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(model.ModelState.IsValid);
    }
}
