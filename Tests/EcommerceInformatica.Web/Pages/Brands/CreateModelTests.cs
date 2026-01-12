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

public class CreateModelTests
{
    private readonly Mock<BrandService> _mockBrandService;
    private readonly Mock<ILogger<CreateModel>> _mockLogger;
    private readonly CreateModel _pageModel;

    public CreateModelTests()
    {
        _mockBrandService = new Mock<BrandService>();
        _mockLogger = new Mock<ILogger<CreateModel>>();
        _pageModel = new CreateModel(_mockBrandService.Object, _mockLogger.Object);
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
    public async Task OnPostAsync_CreatesBrand_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        _pageModel.Brand = new CreateModel.BrandCreateViewModel
        {
            Name = "New Brand",
            Description = "Brand Description"
        };
        _mockBrandService.Setup(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None)).ReturnsAsync(new BrandDto());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Brand created successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockBrandService.Verify(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_MapsViewModelToDtoCorrectly()
    {
        // Arrange
        _pageModel.Brand = new CreateModel.BrandCreateViewModel
        {
            Name = "Test Brand",
            Description = "Test Description"
        };

        BrandCreateDto capturedDto = null;
        _mockBrandService.Setup(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None))
            .Callback<BrandCreateDto, CancellationToken>((dto, ct) => capturedDto = dto)
            .ReturnsAsync(new BrandDto());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(capturedDto);
        Assert.Equal("Test Brand", capturedDto.Name);
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
        _mockBrandService.Verify(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Brand = new CreateModel.BrandCreateViewModel
        {
            Name = "Brand",
            Description = "Description"
        };
        _mockBrandService.Setup(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None)).ThrowsAsync(new Exception("Error"));

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
        _pageModel.Brand = new CreateModel.BrandCreateViewModel
        {
            Name = "Brand",
            Description = "Description"
        };
        var exception = new Exception("Error");
        _mockBrandService.Setup(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None)).ThrowsAsync(exception);

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
    public void BrandCreateViewModel_HasRequiredProperties()
    {
        // Arrange & Act
        var viewModel = new CreateModel.BrandCreateViewModel();

        // Assert
        Assert.NotNull(viewModel.Name);
        Assert.NotNull(viewModel.Description);
    }

    [Fact]
    public void Brand_Property_InitializedByDefault()
    {
        // Arrange & Act
        var pageModel = new CreateModel(_mockBrandService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(pageModel.Brand);
    }

    [Fact]
    public async Task OnPostAsync_AddsModelError_WhenExceptionOccurs()
    {
        // Arrange
        _pageModel.Brand = new CreateModel.BrandCreateViewModel
        {
            Name = "Brand",
            Description = "Description"
        };
        _mockBrandService.Setup(s => s.CreateAsync(It.IsAny<BrandCreateDto>(), CancellationToken.None))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.True(_pageModel.ModelState.ContainsKey(string.Empty));
        Assert.Contains("An error occurred while creating the brand.",
            _pageModel.ModelState[string.Empty].Errors.Select(e => e.ErrorMessage));
    }
}
