using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IProductService productService, ILogger<IndexModel> logger)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading products page");
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Products = await _productService.SearchAsync(SearchTerm);
                _logger.LogInformation("Search completed for term: {SearchTerm}", SearchTerm);
            }
            else
            {
                Products = await _productService.GetAllAsync();
                _logger.LogInformation("All products loaded");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading products");
            Products = new List<ProductDto>();
        }
    }
}
