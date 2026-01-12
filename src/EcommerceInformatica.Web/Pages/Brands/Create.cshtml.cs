using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Brands;

public class CreateModel : PageModel
{
    private readonly BrandService _brandService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(BrandService brandService, ILogger<CreateModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
    public BrandCreateViewModel Brand { get; set; } = new BrandCreateViewModel();

    public IActionResult OnGet()
    {
        return Page();
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
            var brandCreateDto = new BrandCreateDto
            {
                Name = Brand.Name,
                Description = Brand.Description
            };

            await _brandService.CreateAsync(brandCreateDto);
            TempData["SuccessMessage"] = "Brand created successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating brand");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the brand.");
            return Page();
        }
    }

    // ViewModel for Brand Create
    public class BrandCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}
