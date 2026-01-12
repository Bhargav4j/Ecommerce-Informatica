using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Categories;

public class EditModel : PageModel
{
    private readonly CategoryService _categoryService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(CategoryService categoryService, ILogger<EditModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [BindProperty]
    public CategoryEditViewModel Category { get; set; } = new CategoryEditViewModel();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var categoryDto = await _categoryService.GetByIdAsync(id.Value);

            if (categoryDto == null)
            {
                return NotFound();
            }

            // Manual mapping from DTO to ViewModel
            Category = new CategoryEditViewModel
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                IsActive = categoryDto.IsActive
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading category with ID {CategoryId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the category.";
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
            // Manual mapping from ViewModel to DTO
            var categoryUpdateDto = new CategoryUpdateDto
            {
                Name = Category.Name,
                Description = Category.Description,
                IsActive = Category.IsActive
            };

            await _categoryService.UpdateAsync(Category.Id, categoryUpdateDto);
            TempData["SuccessMessage"] = "Category updated successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with ID {CategoryId}", Category.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the category.");
            return Page();
        }
    }

    // ViewModel for Category Edit
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
