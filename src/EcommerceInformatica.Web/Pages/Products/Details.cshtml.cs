using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly ProductService _productService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ProductService productService, ILogger<DetailsModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public ProductDto? Product { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            Product = await _productService.GetByIdAsync(id.Value);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with ID {ProductId}", id);
            TempData["ErrorMessage"] = "An error occurred while retrieving the product.";
            return RedirectToPage("Index");
        }
    }
}
