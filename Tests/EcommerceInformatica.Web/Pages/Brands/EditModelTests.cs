using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Brands;

namespace EcommerceInformatica.Web.Tests.Pages.Brands;

public class EditModelTests
{
    private readonly Mock<BrandService> _mockBrandService;
    private readonly Mock<ILogger<EditModel>> _mockLogger;
    private readonly EditModel _pageModel;

    public EditModelTests()
    {
        _mockBrandService = new Mock<BrandService>();
        _mockLogger = new Mock<ILogger<EditModel>>();
        _pageModel = new EditModel(_mockBrandService.Object, _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _pageModel.OnGetAsync(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockBrandService.Verify(s => s.GetByIdAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenBrandDoesNotExist()
    {
        // Arrange
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync((BrandDto)null);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithMappedViewModel()
    {
        // Arrange
        var brandDto = new BrandDto
        {
            Id = 1,
            Name = "Test Brand",
            Description = "Test Description",
            IsActive = true
        };
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(brandDto);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(1, _pageModel.Brand.Id);
        Assert.Equal("Test Brand", _pageModel.Brand.Name);
        Assert.Equal("Test Description", _pageModel.Brand.Description);
        Assert.True(_pageModel.Brand.IsActive);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockBrandService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_UpdatesBrand_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        _pageModel.Brand = new EditModel.BrandEditViewModel
        {
            Id = 1,
            Name = "Updated Brand",
            Description = "Updated Description",
            IsActive = true
        };
        _mockBrandService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandUpdateDto>(), CancellationToken.None)).ReturnsAsync(new BrandDto());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Brand updated successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockBrandService.Verify(s => s.UpdateAsync(1, It.IsAny<BrandUpdateDto>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_MapsViewModelToDtoCorrectly()
    {
        // Arrange
        _pageModel.Brand = new EditModel.BrandEditViewModel
        {
            Id = 5,
            Name = "Test Brand",
            Description = "Test Description",
            IsActive = false
        };

        BrandUpdateDto capturedDto = null;
        int capturedId = 0;
        _mockBrandService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandUpdateDto>(), CancellationToken.None))
            .Callback<int, BrandUpdateDto, CancellationToken>((id, dto, ct) =>
            {
                capturedId = id;
                capturedDto = dto;
            })
            .ReturnsAsync(new BrandDto());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.Equal(5, capturedId);
        Assert.NotNull(capturedDto);
        Assert.Equal("Test Brand", capturedDto.Name);
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
        _mockBrandService.Verify(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandUpdateDto>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Brand = new EditModel.BrandEditViewModel
        {
            Id = 1,
            Name = "Brand",
            Description = "Description",
            IsActive = true
        };
        _mockBrandService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandUpdateDto>(), CancellationToken.None)).ThrowsAsync(new Exception("Error"));

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
        _pageModel.Brand = new EditModel.BrandEditViewModel
        {
            Id = 1,
            Name = "Brand",
            Description = "Description",
            IsActive = true
        };
        var exception = new Exception("Error");
        _mockBrandService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandUpdateDto>(), CancellationToken.None)).ThrowsAsync(exception);

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
    public void BrandEditViewModel_HasRequiredProperties()
    {
        // Arrange & Act
        var viewModel = new EditModel.BrandEditViewModel();

        // Assert
        Assert.NotNull(viewModel.Name);
        Assert.NotNull(viewModel.Description);
    }
}
