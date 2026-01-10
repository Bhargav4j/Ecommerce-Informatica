using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Categories;

[Authorize]
public class EditModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(ICategoryService categoryService, ILogger<EditModel> logger)
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
            _logger.LogError(ex, "Error occurred while loading edit category page for ID {CategoryId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the category. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            byte[]? pictureBytes = Category.Picture;
            if (Category.PictureFile != null && Category.PictureFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await Category.PictureFile.CopyToAsync(memoryStream);
                pictureBytes = memoryStream.ToArray();
            }

            var updateDto = new CategoryUpdateDto
            {
                CategoryId = Category.CategoryId,
                CategoryName = Category.CategoryName,
                Description = Category.Description,
                Picture = pictureBytes
            };

            var updatedCategory = await _categoryService.UpdateAsync(updateDto);
            if (updatedCategory == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found for update", Category.CategoryId);
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToPage("Index");
            }

            _logger.LogInformation("Category updated successfully: {CategoryName} (ID: {CategoryId})",
                updatedCategory.CategoryName, updatedCategory.CategoryId);

            TempData["SuccessMessage"] = $"Category '{updatedCategory.CategoryName}' updated successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating category: {CategoryName} (ID: {CategoryId})",
                Category.CategoryName, Category.CategoryId);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the category. Please try again.");
            return Page();
        }
    }
}
