using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.ViewModels;

public class ProductViewModel
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Supplier is required")]
    [Display(Name = "Supplier")]
    public int SupplierId { get; set; }

    [Display(Name = "Supplier Name")]
    public string? SupplierName { get; set; }

    [Required(ErrorMessage = "Category is required")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Display(Name = "Category Name")]
    public string? CategoryName { get; set; }

    [Display(Name = "Brand")]
    public int? BrandId { get; set; }

    [Display(Name = "Brand Name")]
    public string? BrandName { get; set; }

    [StringLength(50, ErrorMessage = "Quantity per unit cannot exceed 50 characters")]
    [Display(Name = "Quantity Per Unit")]
    public string? QuantityPerUnit { get; set; }

    [Required(ErrorMessage = "Unit price is required")]
    [Range(0, 999999.99, ErrorMessage = "Unit price must be between 0 and 999,999.99")]
    [DataType(DataType.Currency)]
    [Display(Name = "Unit Price")]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Units in stock is required")]
    [Range(0, short.MaxValue, ErrorMessage = "Units in stock must be a positive number")]
    [Display(Name = "Units In Stock")]
    public short UnitsInStock { get; set; }

    [Required(ErrorMessage = "Units on order is required")]
    [Range(0, short.MaxValue, ErrorMessage = "Units on order must be a positive number")]
    [Display(Name = "Units On Order")]
    public short UnitsOnOrder { get; set; }

    [Required(ErrorMessage = "Reorder level is required")]
    [Range(0, short.MaxValue, ErrorMessage = "Reorder level must be a positive number")]
    [Display(Name = "Reorder Level")]
    public short ReorderLevel { get; set; }

    [Display(Name = "Discontinued")]
    public bool Discontinued { get; set; }
}
