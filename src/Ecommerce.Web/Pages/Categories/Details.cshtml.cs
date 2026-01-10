using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Categories;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICategoryService categoryService, ILogger<DetailsModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public CategoryViewModel Category { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);
            if (categoryDto == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found", id);
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToPage("Index");
            }

            Category = new CategoryViewModel
            {
                CategoryId = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description,
                Picture = categoryDto.Picture
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading category details for ID {CategoryId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the category details. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
