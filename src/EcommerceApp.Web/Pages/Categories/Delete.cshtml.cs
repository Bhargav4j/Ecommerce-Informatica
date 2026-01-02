using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceApp.Web.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ICategoryService categoryService, ILogger<DeleteModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category == null)
        {
            return NotFound();
        }

        try
        {
            await _categoryService.DeleteAsync(Category.Id);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category {CategoryId}", Category.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the category.");
            return Page();
        }
    }
}
