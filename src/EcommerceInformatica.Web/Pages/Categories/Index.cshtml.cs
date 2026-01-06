using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceInformatica.Web.Pages.Categories;

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
            var result = await _categoryService.GetAllAsync();
            Categories = result.Cast<CategoryDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading categories");
            Categories = new List<CategoryDto>();
        }
    }
}
