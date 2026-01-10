using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Brands;

[Authorize]
public class EditModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(IBrandService brandService, ILogger<EditModel> logger)
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
            _logger.LogError(ex, "Error occurred while loading edit brand page for ID {BrandId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the brand. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var updateDto = new BrandUpdateDto
            {
                BrandId = Brand.BrandId,
                BrandName = Brand.BrandName
            };

            var updatedBrand = await _brandService.UpdateAsync(updateDto);
            if (updatedBrand == null)
            {
                _logger.LogWarning("Brand with ID {BrandId} not found for update", Brand.BrandId);
                TempData["ErrorMessage"] = "Brand not found.";
                return RedirectToPage("Index");
            }

            _logger.LogInformation("Brand updated successfully: {BrandName} (ID: {BrandId})",
                updatedBrand.BrandName, updatedBrand.BrandId);

            TempData["SuccessMessage"] = $"Brand '{updatedBrand.BrandName}' updated successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating brand: {BrandName} (ID: {BrandId})",
                Brand.BrandName, Brand.BrandId);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the brand. Please try again.");
            return Page();
        }
    }
}
