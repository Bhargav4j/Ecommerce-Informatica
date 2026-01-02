using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceApp.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICategoryService categoryService, ILogger<IndexModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Categories = await _categoryService.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading categories");
            Categories = new List<CategoryDto>();
        }
    }
}
