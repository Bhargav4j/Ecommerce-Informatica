using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly CategoryService _categoryService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(CategoryService categoryService, ILogger<DeleteModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category == null || Category.Id == 0)
        {
            return NotFound();
        }

        try
        {
            await _categoryService.DeleteAsync(Category.Id);
            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category with ID {CategoryId}", Category.Id);
            TempData["ErrorMessage"] = "An error occurred while deleting the category.";
            return RedirectToPage("Index");
        }
    }
}
