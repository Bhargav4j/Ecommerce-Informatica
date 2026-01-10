using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Brands;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(IBrandService brandService, ILogger<DeleteModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
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
            _logger.LogError(ex, "Error occurred while loading delete brand page for ID {BrandId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the brand. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var deleted = await _brandService.DeleteAsync(Brand.BrandId);
            if (!deleted)
            {
                _logger.LogWarning("Brand with ID {BrandId} not found for deletion", Brand.BrandId);
                TempData["ErrorMessage"] = "Brand not found.";
                return RedirectToPage("Index");
            }

            _logger.LogInformation("Brand deleted successfully: {BrandName} (ID: {BrandId})",
                Brand.BrandName, Brand.BrandId);

            TempData["SuccessMessage"] = $"Brand '{Brand.BrandName}' deleted successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting brand: {BrandName} (ID: {BrandId})",
                Brand.BrandName, Brand.BrandId);
            TempData["ErrorMessage"] = "An error occurred while deleting the brand. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
