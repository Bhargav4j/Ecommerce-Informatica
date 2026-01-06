using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceInformatica.Web.Pages.Products;

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

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var result = await _productService.SearchAsync(SearchTerm);
                Products = result.Cast<ProductDto>();
            }
            else
            {
                var result = await _productService.GetAllAsync();
                Products = result.Cast<ProductDto>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading products");
            Products = new List<ProductDto>();
        }
    }
}
