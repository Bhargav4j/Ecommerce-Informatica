using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Brands;

public class IndexModel : PageModel
{
    private readonly BrandService _brandService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(BrandService brandService, ILogger<IndexModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    public IEnumerable<BrandDto> Brands { get; set; } = new List<BrandDto>();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var allBrands = await _brandService.GetAllAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Brands = allBrands.Where(b =>
                    b.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                    b.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
                );
            }
            else
            {
                Brands = allBrands;
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving brands");
            TempData["ErrorMessage"] = "An error occurred while retrieving brands.";
            Brands = new List<BrandDto>();
            return Page();
        }
    }
}
