using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Brands;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(IBrandService brandService, ILogger<DetailsModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    public BrandViewModel Brand { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var brandDto = await _brandService.GetByIdAsync(id);
            if (brandDto == null)
            {
                _logger.LogWarning("Brand with ID {BrandId} not found", id);
                TempData["ErrorMessage"] = "Brand not found.";
                return RedirectToPage("Index");
            }

            Brand = new BrandViewModel
            {
                BrandId = brandDto.BrandId,
                BrandName = brandDto.BrandName
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading brand details for ID {BrandId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the brand details. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
