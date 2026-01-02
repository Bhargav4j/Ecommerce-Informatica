using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Web.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ICategoryService categoryService, ILogger<CreateModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [BindProperty]
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [StringLength(500)]
    public string? Description { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var createDto = new CategoryCreateDto
            {
                Name = Name,
                Description = Description,
                CreatedBy = "System"
            };

            await _categoryService.CreateAsync(createDto);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the category.");
            return Page();
        }
    }
}
