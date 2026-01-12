using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Brands;

namespace EcommerceInformatica.Web.Tests.Pages.Brands;

public class IndexModelTests
{
    private readonly Mock<BrandService> _mockBrandService;
    private readonly Mock<ILogger<IndexModel>> _mockLogger;
    private readonly IndexModel _pageModel;

    public IndexModelTests()
    {
        _mockBrandService = new Mock<BrandService>();
        _mockLogger = new Mock<ILogger<IndexModel>>();
        _pageModel = new IndexModel(_mockBrandService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithAllBrands_WhenNoSearchString()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Brand 1", Description = "Description 1", IsActive = true },
            new BrandDto { Id = 2, Name = "Brand 2", Description = "Description 2", IsActive = true }
        };
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(2, _pageModel.Brands.Count());
        _mockBrandService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredBrands_WhenSearchStringMatchesName()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Nike", Description = "Sports apparel", IsActive = true },
            new BrandDto { Id = 2, Name = "Adidas", Description = "Athletic wear", IsActive = true },
            new BrandDto { Id = 3, Name = "Puma", Description = "Sports equipment", IsActive = true }
        };
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);
        _pageModel.SearchString = "Nike";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Brands);
        Assert.Equal("Nike", _pageModel.Brands.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithFilteredBrands_WhenSearchStringMatchesDescription()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Nike", Description = "Sports apparel", IsActive = true },
            new BrandDto { Id = 2, Name = "Adidas", Description = "Athletic wear", IsActive = true },
            new BrandDto { Id = 3, Name = "Puma", Description = "Sports equipment", IsActive = true }
        };
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);
        _pageModel.SearchString = "apparel";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Brands);
        Assert.Equal("Nike", _pageModel.Brands.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_SearchIsCaseInsensitive()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Nike", Description = "Sports apparel", IsActive = true },
            new BrandDto { Id = 2, Name = "Adidas", Description = "Athletic wear", IsActive = true }
        };
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);
        _pageModel.SearchString = "nike";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Brands);
        Assert.Equal("Nike", _pageModel.Brands.First().Name);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsEmptyList_WhenNoBrandsMatchSearch()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Nike", Description = "Sports apparel", IsActive = true },
            new BrandDto { Id = 2, Name = "Adidas", Description = "Athletic wear", IsActive = true }
        };
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);
        _pageModel.SearchString = "NonExistentBrand";

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Brands);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_ReturnsPageWithEmptyList()
    {
        // Arrange
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Brands);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnGetAsync_LogsError_WhenExceptionOccurs()
    {
        // Arrange
        var exception = new Exception("Database error");
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(exception);

        // Act
        await _pageModel.OnGetAsync();

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
    public async Task OnGetAsync_ReturnsPageResult_WhenBrandServiceReturnsEmptyList()
    {
        // Arrange
        var brands = new List<BrandDto>();
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Empty(_pageModel.Brands);
    }

    [Fact]
    public void SearchString_Property_CanBeSet()
    {
        // Arrange
        var searchString = "TestSearch";

        // Act
        _pageModel.SearchString = searchString;

        // Assert
        Assert.Equal(searchString, _pageModel.SearchString);
    }

    [Fact]
    public void Brands_Property_InitializedAsEmptyList()
    {
        // Arrange & Act
        var pageModel = new IndexModel(_mockBrandService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(pageModel.Brands);
        Assert.Empty(pageModel.Brands);
    }

    [Fact]
    public async Task OnGetAsync_IncludesInactiveBrands_InResults()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Active Brand", Description = "Active", IsActive = true },
            new BrandDto { Id = 2, Name = "Inactive Brand", Description = "Inactive", IsActive = false }
        };
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(2, _pageModel.Brands.Count());
    }
}
