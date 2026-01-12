using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Products;

public class EditModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly BrandService _brandService;
    private readonly SupplierService _supplierService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ProductService productService,
        CategoryService categoryService,
        BrandService brandService,
        SupplierService supplierService,
        ILogger<EditModel> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _supplierService = supplierService;
        _logger = logger;
    }

    [BindProperty]
    public ProductEditViewModel Product { get; set; } = new ProductEditViewModel();

    public SelectList Categories { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
    public SelectList Brands { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
    public SelectList Suppliers { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var productDto = await _productService.GetByIdAsync(id.Value);

            if (productDto == null)
            {
                return NotFound();
            }

            // Manual mapping from DTO to ViewModel
            Product = new ProductEditViewModel
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                UnitPrice = productDto.UnitPrice,
                Stock = productDto.Stock,
                IsActive = productDto.IsActive,
                CategoryId = productDto.CategoryId,
                BrandId = productDto.BrandId,
                SupplierId = productDto.SupplierId
            };

            await LoadSelectListsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading product with ID {ProductId}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the product.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            return Page();
        }

        try
        {
            // Manual mapping from ViewModel to DTO
            var productUpdateDto = new ProductUpdateDto
            {
                Name = Product.Name,
                Description = Product.Description,
                UnitPrice = Product.UnitPrice,
                Stock = Product.Stock,
                IsActive = Product.IsActive,
                CategoryId = Product.CategoryId,
                BrandId = Product.BrandId,
                SupplierId = Product.SupplierId
            };

            await _productService.UpdateAsync(Product.Id, productUpdateDto);
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with ID {ProductId}", Product.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
            await LoadSelectListsAsync();
            return Page();
        }
    }

    private async Task LoadSelectListsAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        var brands = await _brandService.GetAllAsync();
        var suppliers = await _supplierService.GetAllAsync();

        Categories = new SelectList(categories.Where(c => c.IsActive), "Id", "Name");
        Brands = new SelectList(brands.Where(b => b.IsActive), "Id", "Name");
        Suppliers = new SelectList(suppliers.Where(s => s.IsActive), "Id", "Name");
    }

    // ViewModel for Product Edit
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or greater")]
        public int Stock { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a brand")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a supplier")]
        public int SupplierId { get; set; }
    }
}
