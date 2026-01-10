using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ISupplierService _supplierService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        IProductService productService,
        ICategoryService categoryService,
        ISupplierService supplierService,
        ILogger<IndexModel> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _supplierService = supplierService;
        _logger = logger;
    }

    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public IEnumerable<SupplierDto> Suppliers { get; set; } = new List<SupplierDto>();

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? CategoryFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? SupplierFilter { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            // Load filter options
            Categories = await _categoryService.GetAllAsync();
            Suppliers = await _supplierService.GetAllAsync();

            // Load products based on filters
            if (CategoryFilter.HasValue)
            {
                Products = await _productService.GetByCategoryAsync(CategoryFilter.Value);
            }
            else if (SupplierFilter.HasValue)
            {
                Products = await _productService.GetBySupplierAsync(SupplierFilter.Value);
            }
            else
            {
                Products = await _productService.GetAllAsync();
            }

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Products = Products.Where(p =>
                    p.ProductName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (p.CategoryName?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.SupplierName?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
            }

            _logger.LogInformation("Loaded {Count} products", Products.Count());

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading products");
            TempData["ErrorMessage"] = "An error occurred while loading products. Please try again.";
            return Page();
        }
    }
}
