using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Products;

public class DeleteModel : PageModel
{
    private readonly ProductService _productService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ProductService productService, ILogger<DeleteModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (Product == null || Product.Id == 0)
        {
            return NotFound();
        }

        try
        {
            await _productService.DeleteAsync(Product.Id);
            TempData["SuccessMessage"] = "Product deleted successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with ID {ProductId}", Product.Id);
            TempData["ErrorMessage"] = "An error occurred while deleting the product.";
            return RedirectToPage("Index");
        }
    }
}
