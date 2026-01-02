using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Web.Pages.Categories;

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
    public int Id { get; set; }

    [BindProperty]
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [StringLength(500)]
    public string? Description { get; set; }

    [BindProperty]
    public bool IsActive { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
            IsActive = category.IsActive;

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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var updateDto = new CategoryUpdateDto
            {
                Name = Name,
                Description = Description,
                IsActive = IsActive,
                ModifiedBy = "System"
            };

            await _categoryService.UpdateAsync(Id, updateDto);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category {CategoryId}", Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the category.");
            return Page();
        }
    }
}
