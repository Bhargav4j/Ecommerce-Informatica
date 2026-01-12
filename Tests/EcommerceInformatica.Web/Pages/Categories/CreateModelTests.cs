using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Categories;

namespace EcommerceInformatica.Web.Tests.Pages.Categories;

public class CreateModelTests
{
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<ILogger<CreateModel>> _mockLogger;
    private readonly CreateModel _pageModel;

    public CreateModelTests()
    {
        _mockCategoryService = new Mock<CategoryService>();
        _mockLogger = new Mock<ILogger<CreateModel>>();
        _pageModel = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public void OnGet_ReturnsPageResult()
    {
        // Act
        var result = _pageModel.OnGet();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_CreatesCategory_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        _pageModel.Category = new CreateModel.CategoryCreateViewModel
        {
            Name = "New Category",
            Description = "Category Description"
        };
        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None)).ReturnsAsync(new CategoryDto());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Category created successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockCategoryService.Verify(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_MapsViewModelToDtoCorrectly()
    {
        // Arrange
        _pageModel.Category = new CreateModel.CategoryCreateViewModel
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        CategoryCreateDto capturedDto = null;
        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None))
            .Callback<CategoryCreateDto, CancellationToken>((dto, ct) => capturedDto = dto)
            .ReturnsAsync(new CategoryDto());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(capturedDto);
        Assert.Equal("Test Category", capturedDto.Name);
        Assert.Equal("Test Description", capturedDto.Description);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsPage_WhenModelStateIsInvalid()
    {
        // Arrange
        _pageModel.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        _mockCategoryService.Verify(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Category = new CreateModel.CategoryCreateViewModel
        {
            Name = "Category",
            Description = "Description"
        };
        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.True(_pageModel.ModelState.ErrorCount > 0);
    }

    [Fact]
    public async Task OnPostAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        _pageModel.Category = new CreateModel.CategoryCreateViewModel
        {
            Name = "Category",
            Description = "Description"
        };
        var exception = new Exception("Error");
        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None)).ThrowsAsync(exception);

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void CategoryCreateViewModel_HasRequiredProperties()
    {
        // Arrange & Act
        var viewModel = new CreateModel.CategoryCreateViewModel();

        // Assert
        Assert.NotNull(viewModel.Name);
        Assert.NotNull(viewModel.Description);
    }

    [Fact]
    public void Category_Property_InitializedByDefault()
    {
        // Arrange & Act
        var pageModel = new CreateModel(_mockCategoryService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(pageModel.Category);
    }

    [Fact]
    public async Task OnPostAsync_AddsModelError_WhenExceptionOccurs()
    {
        // Arrange
        _pageModel.Category = new CreateModel.CategoryCreateViewModel
        {
            Name = "Category",
            Description = "Description"
        };
        _mockCategoryService.Setup(s => s.CreateAsync(It.IsAny<CategoryCreateDto>(), CancellationToken.None))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.True(_pageModel.ModelState.ContainsKey(string.Empty));
        Assert.Contains("An error occurred while creating the category.",
            _pageModel.ModelState[string.Empty].Errors.Select(e => e.ErrorMessage));
    }
}
