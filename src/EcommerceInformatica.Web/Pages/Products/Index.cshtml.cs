using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly ProductService _productService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ProductService productService, ILogger<IndexModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var allProducts = await _productService.GetAllAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Products = allProducts.Where(p =>
                    p.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                    (p.CategoryName != null && p.CategoryName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) ||
                    (p.BrandName != null && p.BrandName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                );
            }
            else
            {
                Products = allProducts;
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products");
            TempData["ErrorMessage"] = "An error occurred while retrieving products.";
            Products = new List<ProductDto>();
            return Page();
        }
    }
}
