using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Web.Pages.Products;

namespace EcommerceInformatica.Web.Tests.Pages.Products;

public class EditModelTests
{
    private readonly Mock<ProductService> _mockProductService;
    private readonly Mock<CategoryService> _mockCategoryService;
    private readonly Mock<BrandService> _mockBrandService;
    private readonly Mock<SupplierService> _mockSupplierService;
    private readonly Mock<ILogger<EditModel>> _mockLogger;
    private readonly EditModel _pageModel;

    public EditModelTests()
    {
        _mockProductService = new Mock<ProductService>();
        _mockCategoryService = new Mock<CategoryService>();
        _mockBrandService = new Mock<BrandService>();
        _mockSupplierService = new Mock<SupplierService>();
        _mockLogger = new Mock<ILogger<EditModel>>();
        _pageModel = new EditModel(
            _mockProductService.Object,
            _mockCategoryService.Object,
            _mockBrandService.Object,
            _mockSupplierService.Object,
            _mockLogger.Object);
        _pageModel.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _pageModel.OnGetAsync(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockProductService.Verify(s => s.GetByIdAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync((ProductDto)null);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPageResult_WithMappedViewModel()
    {
        // Arrange
        var productDto = new ProductDto
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            UnitPrice = 99.99m,
            Stock = 50,
            IsActive = true,
            CategoryId = 2,
            BrandId = 3,
            SupplierId = 4
        };
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(productDto);
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<CategoryDto> { new CategoryDto { Id = 2, Name = "Cat", IsActive = true } });
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(1, _pageModel.Product.Id);
        Assert.Equal("Test Product", _pageModel.Product.Name);
        Assert.Equal("Test Description", _pageModel.Product.Description);
        Assert.Equal(99.99m, _pageModel.Product.UnitPrice);
        Assert.Equal(50, _pageModel.Product.Stock);
        Assert.True(_pageModel.Product.IsActive);
        Assert.Equal(2, _pageModel.Product.CategoryId);
        Assert.Equal(3, _pageModel.Product.BrandId);
        Assert.Equal(4, _pageModel.Product.SupplierId);
    }

    [Fact]
    public async Task OnGetAsync_LoadsSelectLists()
    {
        // Arrange
        var productDto = new ProductDto { Id = 1, Name = "Product", CategoryId = 1, BrandId = 1, SupplierId = 1 };
        var categories = new List<CategoryDto> { new CategoryDto { Id = 1, Name = "Category", IsActive = true } };
        var brands = new List<BrandDto> { new BrandDto { Id = 1, Name = "Brand", IsActive = true } };
        var suppliers = new List<SupplierDto> { new SupplierDto { Id = 1, Name = "Supplier", IsActive = true } };

        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(productDto);
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(categories);
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(brands);
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(suppliers);

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Single(_pageModel.Categories);
        Assert.Single(_pageModel.Brands);
        Assert.Single(_pageModel.Suppliers);
    }

    [Fact]
    public async Task OnGetAsync_HandlesException_RedirectsToIndex()
    {
        // Arrange
        _mockProductService.Setup(s => s.GetByIdAsync(1, CancellationToken.None)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _pageModel.OnGetAsync(1);

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.NotNull(_pageModel.TempData["ErrorMessage"]);
    }

    [Fact]
    public async Task OnPostAsync_UpdatesProduct_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        _pageModel.Product = new EditModel.ProductEditViewModel
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            UnitPrice = 149.99m,
            Stock = 75,
            IsActive = true,
            CategoryId = 2,
            BrandId = 3,
            SupplierId = 4
        };
        _mockProductService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductUpdateDto>(), CancellationToken.None)).ReturnsAsync(new ProductDto());

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("Index", redirectResult.PageName);
        Assert.Equal("Product updated successfully.", _pageModel.TempData["SuccessMessage"]);
        _mockProductService.Verify(s => s.UpdateAsync(1, It.IsAny<ProductUpdateDto>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_MapsViewModelToDtoCorrectly()
    {
        // Arrange
        _pageModel.Product = new EditModel.ProductEditViewModel
        {
            Id = 5,
            Name = "Test Product",
            Description = "Test Description",
            UnitPrice = 49.99m,
            Stock = 100,
            IsActive = false,
            CategoryId = 2,
            BrandId = 3,
            SupplierId = 4
        };

        ProductUpdateDto capturedDto = null;
        int capturedId = 0;
        _mockProductService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductUpdateDto>(), CancellationToken.None))
            .Callback<int, ProductUpdateDto, CancellationToken>((id, dto, ct) =>
            {
                capturedId = id;
                capturedDto = dto;
            })
            .ReturnsAsync(new ProductDto());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        Assert.Equal(5, capturedId);
        Assert.NotNull(capturedDto);
        Assert.Equal("Test Product", capturedDto.Name);
        Assert.Equal("Test Description", capturedDto.Description);
        Assert.Equal(49.99m, capturedDto.UnitPrice);
        Assert.Equal(100, capturedDto.Stock);
        Assert.False(capturedDto.IsActive);
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
        _mockProductService.Verify(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductUpdateDto>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_LoadsSelectLists_WhenModelStateIsInvalid()
    {
        // Arrange
        _pageModel.ModelState.AddModelError("Name", "Name is required");
        _mockCategoryService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<CategoryDto>());
        _mockBrandService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<BrandDto>());
        _mockSupplierService.Setup(s => s.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<SupplierDto>());

        // Act
        await _pageModel.OnPostAsync();

        // Assert
        _mockCategoryService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
        _mockBrandService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
        _mockSupplierService.Verify(s => s.GetAllAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_HandlesException_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Product = new EditModel.ProductEditViewModel
        {
            Id = 1,
            Name = "Product",
            Description = "Description",
            UnitPrice = 10.00m,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };
        _mockProductService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductUpdateDto>(), CancellationToken.None)).ThrowsAsync(new Exception("Error"));
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
        _pageModel.Product = new EditModel.ProductEditViewModel
        {
            Id = 1,
            Name = "Product",
            Description = "Description",
            UnitPrice = 10.00m,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };
        var exception = new Exception("Error");
        _mockProductService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductUpdateDto>(), CancellationToken.None)).ThrowsAsync(exception);
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
}
