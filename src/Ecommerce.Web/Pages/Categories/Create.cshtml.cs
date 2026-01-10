using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Categories;

[Authorize]
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
    public CategoryViewModel Category { get; set; } = new();

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
            byte[]? pictureBytes = null;
            if (Category.PictureFile != null && Category.PictureFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await Category.PictureFile.CopyToAsync(memoryStream);
                pictureBytes = memoryStream.ToArray();
            }

            var createDto = new CategoryCreateDto
            {
                CategoryName = Category.CategoryName,
                Description = Category.Description,
                Picture = pictureBytes
            };

            var createdCategory = await _categoryService.CreateAsync(createDto);
            _logger.LogInformation("Category created successfully: {CategoryName} (ID: {CategoryId})",
                createdCategory.CategoryName, createdCategory.CategoryId);

            TempData["SuccessMessage"] = $"Category '{createdCategory.CategoryName}' created successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating category: {CategoryName}", Category.CategoryName);
            ModelState.AddModelError(string.Empty, "An error occurred while creating the category. Please try again.");
            return Page();
        }
    }
}
