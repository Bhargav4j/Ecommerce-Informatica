using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ISupplierService _supplierService;
    private readonly IBrandService _brandService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        IProductService productService,
        ICategoryService categoryService,
        ISupplierService supplierService,
        IBrandService brandService,
        ILogger<CreateModel> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _supplierService = supplierService;
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; } = new();

    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public IEnumerable<SupplierDto> Suppliers { get; set; } = new List<SupplierDto>();
    public IEnumerable<BrandDto> Brands { get; set; } = new List<BrandDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await LoadDropdownsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading create product page");
            TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return Page();
        }

        try
        {
            // Manual mapping from ViewModel to CreateDto
            var createDto = new ProductCreateDto
            {
                ProductName = Product.ProductName,
                SupplierId = Product.SupplierId,
                CategoryId = Product.CategoryId,
                BrandId = Product.BrandId,
                QuantityPerUnit = Product.QuantityPerUnit,
                UnitPrice = Product.UnitPrice,
                UnitsInStock = Product.UnitsInStock,
                UnitsOnOrder = Product.UnitsOnOrder,
                ReorderLevel = Product.ReorderLevel,
                Discontinued = Product.Discontinued
            };

            var createdProduct = await _productService.CreateAsync(createDto);

            _logger.LogInformation("Product created successfully: {ProductName} (ID: {ProductId})",
                createdProduct.ProductName, createdProduct.ProductId);

            TempData["SuccessMessage"] = $"Product '{createdProduct.ProductName}' created successfully!";

            return RedirectToPage("Index");
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Validation error occurred while creating product");
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadDropdownsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating product: {ProductName}", Product.ProductName);
            ModelState.AddModelError(string.Empty, "An error occurred while creating the product. Please try again.");
            await LoadDropdownsAsync();
            return Page();
        }
    }

    private async Task LoadDropdownsAsync()
    {
        Categories = await _categoryService.GetAllAsync();
        Suppliers = await _supplierService.GetAllAsync();
        Brands = await _brandService.GetAllAsync();
    }
}
