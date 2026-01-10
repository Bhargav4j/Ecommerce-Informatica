using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(
        IProductService productService,
        ILogger<DetailsModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

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
            _logger.LogError(ex, "Error occurred while loading product details for ID {ProductId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the product details. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
