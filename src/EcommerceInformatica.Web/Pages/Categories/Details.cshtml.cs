using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Categories;

public class DetailsModel : PageModel
{
    private readonly CategoryService _categoryService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(CategoryService categoryService, ILogger<DetailsModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public CategoryDto? Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            Category = await _categoryService.GetByIdAsync(id.Value);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving category with ID {CategoryId}", id);
            TempData["ErrorMessage"] = "An error occurred while retrieving the category.";
            return RedirectToPage("Index");
        }
    }
}
