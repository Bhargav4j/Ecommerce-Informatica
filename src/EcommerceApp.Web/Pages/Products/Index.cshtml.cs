using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceApp.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IProductService productService, ILogger<IndexModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Products = await _productService.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading products");
            Products = new List<ProductDto>();
        }
    }
}
