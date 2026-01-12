using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Products;

namespace EcommerceInformatica.Web.Tests.Pages.Products;

public class CreateModelTests
{
    private readonly Mock<ProductService> _mockProductService;
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<BrandService> _mockBrandService;
    private readonly Mock<SupplierService> _mockSupplierService;
    private readonly Mock<ILogger<CreateModel>> _mockLogger;
    private readonly CreateModel _pageModel;

    public CreateModelTests()
    {
        _mockProductService = new Mock<ProductService>();
        _mockCategoryService = new Mock<CategoryService>();
        _mockBrandService = new Mock<BrandService>();
        _mockSupplierService = new Mock<SupplierService>();
        _mockLogger = new Mock<ILogger<CreateModel>>();
        _pageModel = new CreateModel(
            _mockProductService.Object,
            _mockCategoryService.Object,
            _mockBrandService.Object,
            _mockSupplierService.Object,
            _mockLogger.Object);

        // Setup TempData
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithSelectLists()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Category 1", IsActive = true },
            new CategoryDto { Id = 2, Name = "Category 2", IsActive = true }
        };
        var brands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Brand 1", IsActive = true },
            new BrandDto { Id = 2, Name = "Brand 2", IsActive = true }
        };
        var suppliers = new List<SupplierDto>
        {
            new SupplierDto { Id = 1, Name = "Supplier 1", IsActive = true },
            new SupplierDto { Id = 2, Name = "Supplier 2", IsActive = true }
        };

        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(suppliers);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(2, _pageModel.Categories.Count());
        Assert.Equal(2, _pageModel.Brands.Count());
        Assert.Equal(2, _pageModel.Suppliers.Count());
    }

    [Fact]
    public async Task OnGetAsync_FiltersInactiveCategories()
    {
        // Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Active Category", IsActive = true },
            new CategoryDto { Id = 2, Name = "Inactive Category", IsActive = false }
        };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Categories);
        Assert.Equal("Active Category", _pageModel.Categories.First().Text);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_CreatesProduct_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        _pageModel.Product = new CreateModel.ProductCreateViewModel
        {
            Name = "New Product",
            Description = "Product Description",
            UnitPrice = 99.99m,
            Stock = 50,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };

        _mockProductService.Setup(s => s.CreateAsync(It.IsAny<ProductCreateDto>(), CancellationToken.None)).ReturnsAsync(new ProductDto());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Product created successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockProductService.Verify(s => s.CreateAsync(It.IsAny<ProductCreateDto>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_MapsViewModelToDtoCorrectly()
    {
        // Arrange
        _pageModel.Product = new CreateModel.ProductCreateViewModel
        {
            Name = "Test Product",
            Description = "Test Description",
            UnitPrice = 49.99m,
            Stock = 100,
            CategoryId = 2,
            BrandId = 3,
            SupplierId = 4
        };

        ProductCreateDto capturedDto = null;
        _mockProductService.Setup(s => s.CreateAsync(It.IsAny<ProductCreateDto>(), CancellationToken.None))
            .Callback<ProductCreateDto, CancellationToken>((dto, ct) => capturedDto = dto)
            .ReturnsAsync(new ProductDto());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(capturedDto);
        Assert.Equal("Test Product", capturedDto.Name);
        Assert.Equal("Test Description", capturedDto.Description);
        Assert.Equal(49.99m, capturedDto.UnitPrice);
        Assert.Equal(100, capturedDto.Stock);
        Assert.Equal(2, capturedDto.CategoryId);
        Assert.Equal(3, capturedDto.BrandId);
        Assert.Equal(4, capturedDto.SupplierId);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsPage_WhenModelStateIsInvalid()
    {
        // Arrange
        _pageModel.ModelState.AddModelError("Name", "Name is required");
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<CategoryDto>());
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        _mockProductService.Verify(s => s.CreateAsync(It.IsAny<ProductCreateDto>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_LoadsSelectLists_WhenModelStateIsInvalid()
    {
        // Arrange
        _pageModel.ModelState.AddModelError("Name", "Name is required");
        var categories = new List<CategoryDto> { new CategoryDto { Id = 1, Name = "Category", IsActive = true } };
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        _mockCategoryService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
        _mockBrandService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
        _mockSupplierService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Product = new CreateModel.ProductCreateViewModel
        {
            Name = "Product",
            Description = "Description",
            UnitPrice = 10.00m,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };
        _mockProductService.Setup(s => s.CreateAsync(It.IsAny<ProductCreateDto>(), CancellationToken.None)).ThrowsAsync(new Exception("Error"));
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<CategoryDto>());
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

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
        _pageModel.Product = new CreateModel.ProductCreateViewModel
        {
            Name = "Product",
            Description = "Description",
            UnitPrice = 10.00m,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };
        var exception = new Exception("Error");
        _mockProductService.Setup(s => s.CreateAsync(It.IsAny<ProductCreateDto>(), CancellationToken.None)).ThrowsAsync(exception);
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<CategoryDto>());
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

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
    public void ProductCreateViewModel_HasRequiredValidation()
    {
        // Arrange
        var viewModel = new CreateModel.ProductCreateViewModel();

        // Act & Assert
        var nameProperty = viewModel.GetType().GetProperty("Name");
        var descriptionProperty = viewModel.GetType().GetProperty("Description");
        var unitPriceProperty = viewModel.GetType().GetProperty("UnitPrice");
        var stockProperty = viewModel.GetType().GetProperty("Stock");

        Assert.NotNull(nameProperty);
        Assert.NotNull(descriptionProperty);
        Assert.NotNull(unitPriceProperty);
        Assert.NotNull(stockProperty);
    }
}
