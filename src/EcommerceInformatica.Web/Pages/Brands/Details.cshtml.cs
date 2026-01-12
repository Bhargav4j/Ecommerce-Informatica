using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Brands;

public class DetailsModel : PageModel
{
    private readonly BrandService _brandService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(BrandService brandService, ILogger<DetailsModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    public BrandDto? Brand { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            Brand = await _brandService.GetByIdAsync(id.Value);

            if (Brand == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving brand with ID {BrandId}", id);
            TempData["ErrorMessage"] = "An error occurred while retrieving the brand.";
            return RedirectToPage("Index");
        }
    }
}
