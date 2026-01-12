using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Services;

namespace EcommerceInformatica.Web.Pages.Products;

public class CreateModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly BrandService _brandService;
    private readonly SupplierService _supplierService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        ProductService productService,
        CategoryService categoryService,
        BrandService brandService,
        SupplierService supplierService,
        ILogger<CreateModel> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _supplierService = supplierService;
        _logger = logger;
    }

    [BindProperty]
    public ProductCreateViewModel Product { get; set; } = new ProductCreateViewModel();

    public SelectList Categories { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
    public SelectList Brands { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
    public SelectList Suppliers { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await LoadSelectListsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading create product page");
            TempData["ErrorMessage"] = "An error occurred while loading the page.";
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
            var productCreateDto = new ProductCreateDto
            {
                Name = Product.Name,
                Description = Product.Description,
                UnitPrice = Product.UnitPrice,
                Stock = Product.Stock,
                CategoryId = Product.CategoryId,
                BrandId = Product.BrandId,
                SupplierId = Product.SupplierId
            };

            await _productService.CreateAsync(productCreateDto);
            TempData["SuccessMessage"] = "Product created successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
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

    // ViewModel for Product Create
    public class ProductCreateViewModel
    {
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
