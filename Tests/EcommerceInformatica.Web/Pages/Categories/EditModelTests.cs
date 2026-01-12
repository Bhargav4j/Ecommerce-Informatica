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

public class EditModelTests
{
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<ILogger<EditModel>> _mockLogger;
    private readonly EditModel _pageModel;

    public EditModelTests()
    {
        _mockCategoryService = new Mock<CategoryService>();
        _mockLogger = new Mock<ILogger<EditModel>>();
        _pageModel = new EditModel(_mockCategoryService.Object, _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _pageModel.OnGetAsync(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockCategoryService.Verify(s => s.GetByIdAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync((CategoryDto)null);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithMappedViewModel()
    {
        // Arrange
        var categoryDto = new CategoryDto
        {
            Id = 1,
            Name = "Test Category",
            Description = "Test Description",
            IsActive = true
        };
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(categoryDto);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(1, _pageModel.Category.Id);
        Assert.Equal("Test Category", _pageModel.Category.Name);
        Assert.Equal("Test Description", _pageModel.Category.Description);
        Assert.True(_pageModel.Category.IsActive);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_UpdatesCategory_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        _pageModel.Category = new EditModel.CategoryEditViewModel
        {
            Id = 1,
            Name = "Updated Category",
            Description = "Updated Description",
            IsActive = true
        };
        _mockCategoryService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryUpdateDto>(), CancellationToken.None)).ReturnsAsync(new CategoryDto());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Category updated successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockCategoryService.Verify(s => s.UpdateAsync(1, It.IsAny<CategoryUpdateDto>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_MapsViewModelToDtoCorrectly()
    {
        // Arrange
        _pageModel.Category = new EditModel.CategoryEditViewModel
        {
            Id = 5,
            Name = "Test Category",
            Description = "Test Description",
            IsActive = false
        };

        CategoryUpdateDto capturedDto = null;
        int capturedId = 0;
        _mockCategoryService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryUpdateDto>(), CancellationToken.None))
            .Callback<int, CategoryUpdateDto, CancellationToken>((id, dto, ct) =>
            {
                capturedId = id;
                capturedDto = dto;
            })
            .ReturnsAsync(new CategoryDto());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.Equal(5, capturedId);
        Assert.NotNull(capturedDto);
        Assert.Equal("Test Category", capturedDto.Name);
        Assert.Equal("Test Description", capturedDto.Description);
        Assert.False(capturedDto.IsActive);
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
        _mockCategoryService.Verify(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryUpdateDto>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Category = new EditModel.CategoryEditViewModel
        {
            Id = 1,
            Name = "Category",
            Description = "Description",
            IsActive = true
        };
        _mockCategoryService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryUpdateDto>(), CancellationToken.None)).ThrowsAsync(new Exception("Error"));

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
        _pageModel.Category = new EditModel.CategoryEditViewModel
        {
            Id = 1,
            Name = "Category",
            Description = "Description",
            IsActive = true
        };
        var exception = new Exception("Error");
        _mockCategoryService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryUpdateDto>(), CancellationToken.None)).ThrowsAsync(exception);

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
    public void CategoryEditViewModel_HasRequiredProperties()
    {
        // Arrange & Act
        var viewModel = new EditModel.CategoryEditViewModel();

        // Assert
        Assert.NotNull(viewModel.Name);
        Assert.NotNull(viewModel.Description);
    }
}
