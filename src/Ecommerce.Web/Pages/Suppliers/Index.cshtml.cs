using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Suppliers;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ISupplierService _supplierService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ISupplierService supplierService, ILogger<IndexModel> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
    }

    public IEnumerable<SupplierDto> Suppliers { get; set; } = new List<SupplierDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Suppliers = await _supplierService.GetAllAsync();
            _logger.LogInformation("Loaded {Count} suppliers", Suppliers.Count());
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading suppliers");
            TempData["ErrorMessage"] = "An error occurred while loading suppliers. Please try again.";
            return Page();
        }
    }
}
