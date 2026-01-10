using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Brands;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IBrandService brandService, ILogger<IndexModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    public IEnumerable<BrandDto> Brands { get; set; } = new List<BrandDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Brands = await _brandService.GetAllAsync();
            _logger.LogInformation("Loaded {Count} brands", Brands.Count());
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading brands");
            TempData["ErrorMessage"] = "An error occurred while loading brands. Please try again.";
            return Page();
        }
    }
}
