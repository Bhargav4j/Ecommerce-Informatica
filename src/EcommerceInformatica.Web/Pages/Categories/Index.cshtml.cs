using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly CategoryService _categoryService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(CategoryService categoryService, ILogger<IndexModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var allCategories = await _categoryService.GetAllAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Categories = allCategories.Where(c =>
                    c.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                    c.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
                );
            }
            else
            {
                Categories = allCategories;
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving categories");
            TempData["ErrorMessage"] = "An error occurred while retrieving categories.";
            Categories = new List<CategoryDto>();
            return Page();
        }
    }
}
