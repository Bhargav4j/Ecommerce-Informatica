using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly CategoryService _categoryService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(CategoryService categoryService, ILogger<CreateModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [BindProperty]
    public CategoryCreateViewModel Category { get; set; } = new CategoryCreateViewModel();

    public IActionResult OnGet()
    {
        return Page();
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
            var categoryCreateDto = new CategoryCreateDto
            {
                Name = Category.Name,
                Description = Category.Description
            };

            await _categoryService.CreateAsync(categoryCreateDto);
            TempData["SuccessMessage"] = "Category created successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the category.");
            return Page();
        }
    }

    // ViewModel for Category Create
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}
