using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Brands;

public class DeleteModel : PageModel
{
    private readonly BrandService _brandService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(BrandService brandService, ILogger<DeleteModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (Brand == null || Brand.Id == 0)
        {
            return NotFound();
        }

        try
        {
            await _brandService.DeleteAsync(Brand.Id);
            TempData["SuccessMessage"] = "Brand deleted successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting brand with ID {BrandId}", Brand.Id);
            TempData["ErrorMessage"] = "An error occurred while deleting the brand.";
            return RedirectToPage("Index");
        }
    }
}
