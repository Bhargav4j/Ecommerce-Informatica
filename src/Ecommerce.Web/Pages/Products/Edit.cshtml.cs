using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products;

[Authorize]
public class EditModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ISupplierService _supplierService;
    private readonly IBrandService _brandService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        IProductService productService,
        ICategoryService categoryService,
        ISupplierService supplierService,
        IBrandService brandService,
        ILogger<EditModel> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _supplierService = supplierService;
        _brandService = brandService;
        _logger = logger;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; } = new();

    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public IEnumerable<SupplierDto> Suppliers { get; set; } = new List<SupplierDto>();
    public IEnumerable<BrandDto> Brands { get; set; } = new List<BrandDto>();

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

            await LoadDropdownsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading edit product page for ID {ProductId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the product. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return Page();
        }

        try
        {
            // Manual mapping from ViewModel to UpdateDto
            var updateDto = new ProductUpdateDto
            {
                ProductId = Product.ProductId,
                ProductName = Product.ProductName,
                SupplierId = Product.SupplierId,
                CategoryId = Product.CategoryId,
                BrandId = Product.BrandId,
                QuantityPerUnit = Product.QuantityPerUnit,
                UnitPrice = Product.UnitPrice,
                UnitsInStock = Product.UnitsInStock,
                UnitsOnOrder = Product.UnitsOnOrder,
                ReorderLevel = Product.ReorderLevel,
                Discontinued = Product.Discontinued
            };

            var updatedProduct = await _productService.UpdateAsync(updateDto);

            if (updatedProduct == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update", Product.ProductId);
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToPage("Index");
            }

            _logger.LogInformation("Product updated successfully: {ProductName} (ID: {ProductId})",
                updatedProduct.ProductName, updatedProduct.ProductId);

            TempData["SuccessMessage"] = $"Product '{updatedProduct.ProductName}' updated successfully!";

            return RedirectToPage("Index");
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Validation error occurred while updating product");
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadDropdownsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product: {ProductName} (ID: {ProductId})",
                Product.ProductName, Product.ProductId);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the product. Please try again.");
            await LoadDropdownsAsync();
            return Page();
        }
    }

    private async Task LoadDropdownsAsync()
    {
        Categories = await _categoryService.GetAllAsync();
        Suppliers = await _supplierService.GetAllAsync();
        Brands = await _brandService.GetAllAsync();
    }
}
