using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Categories;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICategoryService categoryService, ILogger<IndexModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Categories = await _categoryService.GetAllAsync();
            _logger.LogInformation("Loaded {Count} categories", Categories.Count());
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading categories");
            TempData["ErrorMessage"] = "An error occurred while loading categories. Please try again.";
            return Page();
        }
    }
}
