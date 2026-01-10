using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Categories;

[Authorize]
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
            _logger.LogError(ex, "Error occurred while loading delete category page for ID {CategoryId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the category. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var deleted = await _categoryService.DeleteAsync(Category.CategoryId);
            if (!deleted)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found for deletion", Category.CategoryId);
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToPage("Index");
            }

            _logger.LogInformation("Category deleted successfully: {CategoryName} (ID: {CategoryId})",
                Category.CategoryName, Category.CategoryId);

            TempData["SuccessMessage"] = $"Category '{Category.CategoryName}' deleted successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting category: {CategoryName} (ID: {CategoryId})",
                Category.CategoryName, Category.CategoryId);
            TempData["ErrorMessage"] = "An error occurred while deleting the category. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
