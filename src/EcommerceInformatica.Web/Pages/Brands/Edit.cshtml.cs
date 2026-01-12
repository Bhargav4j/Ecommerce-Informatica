using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Brands;

public class EditModel : PageModel
{
    private readonly BrandService _brandService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(BrandService brandService, ILogger<EditModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
    public BrandEditViewModel Brand { get; set; } = new BrandEditViewModel();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var brandDto = await _brandService.GetByIdAsync(id.Value);

            if (brandDto == null)
            {
                return NotFound();
            }

            // Manual mapping from DTO to ViewModel
            Brand = new BrandEditViewModel
            {
                Id = brandDto.Id,
                Name = brandDto.Name,
                Description = brandDto.Description,
                IsActive = brandDto.IsActive
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading brand with ID {BrandId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the brand.";
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
            // Manual mapping from ViewModel to DTO
            var brandUpdateDto = new BrandUpdateDto
            {
                Name = Brand.Name,
                Description = Brand.Description,
                IsActive = Brand.IsActive
            };

            await _brandService.UpdateAsync(Brand.Id, brandUpdateDto);
            TempData["SuccessMessage"] = "Brand updated successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating brand with ID {BrandId}", Brand.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the brand.");
            return Page();
        }
    }

    // ViewModel for Brand Edit
    public class BrandEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
