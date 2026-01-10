using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(
        IProductService productService,
        ILogger<DeleteModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var productDto = await _productService.GetByIdAsync(id);

            if (productDto == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", id);
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToPage("Index");
            }

            // Manual mapping from DTO to ViewModel
            Product = new ProductViewModel
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                SupplierId = productDto.SupplierId,
                SupplierName = productDto.SupplierName,
                CategoryId = productDto.CategoryId,
                CategoryName = productDto.CategoryName,
                BrandId = productDto.BrandId,
                BrandName = productDto.BrandName,
                QuantityPerUnit = productDto.QuantityPerUnit,
                UnitPrice = productDto.UnitPrice,
                UnitsInStock = productDto.UnitsInStock,
                UnitsOnOrder = productDto.UnitsOnOrder,
                ReorderLevel = productDto.ReorderLevel,
                Discontinued = productDto.Discontinued
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading delete product page for ID {ProductId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the product. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var deleted = await _productService.DeleteAsync(Product.ProductId);

            if (!deleted)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for deletion", Product.ProductId);
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToPage("Index");
            }

            _logger.LogInformation("Product deleted successfully: {ProductName} (ID: {ProductId})",
                Product.ProductName, Product.ProductId);

            TempData["SuccessMessage"] = $"Product '{Product.ProductName}' deleted successfully!";

            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product: {ProductName} (ID: {ProductId})",
                Product.ProductName, Product.ProductId);
            TempData["ErrorMessage"] = "An error occurred while deleting the product. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
