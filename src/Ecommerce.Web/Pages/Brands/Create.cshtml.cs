using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Brands;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(IBrandService brandService, ILogger<CreateModel> logger)
    {
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
    public BrandViewModel Brand { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var createDto = new BrandCreateDto
            {
                BrandName = Brand.BrandName
            };

            var createdBrand = await _brandService.CreateAsync(createDto);
            _logger.LogInformation("Brand created successfully: {BrandName} (ID: {BrandId})",
                createdBrand.BrandName, createdBrand.BrandId);

            TempData["SuccessMessage"] = $"Brand '{createdBrand.BrandName}' created successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating brand: {BrandName}", Brand.BrandName);
            ModelState.AddModelError(string.Empty, "An error occurred while creating the brand. Please try again.");
            return Page();
        }
    }
}
