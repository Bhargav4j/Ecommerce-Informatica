using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceApp.Web.Pages.Categories;

public class DetailsModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICategoryService categoryService, ILogger<DetailsModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public CategoryDto? Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            Category = await _categoryService.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading category {CategoryId}", id);
            return NotFound();
        }
    }
}
