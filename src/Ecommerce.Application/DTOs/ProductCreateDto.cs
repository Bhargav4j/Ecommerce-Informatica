using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(40, ErrorMessage = "Product name cannot exceed 40 characters")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Supplier ID is required")]
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    public int CategoryId { get; set; }

    public int? BrandId { get; set; }

    [StringLength(20, ErrorMessage = "Quantity per unit cannot exceed 20 characters")]
    public string? QuantityPerUnit { get; set; }

    [Required(ErrorMessage = "Unit price is required")]
    [Range(0, 999999.99, ErrorMessage = "Unit price must be between 0 and 999999.99")]
    public decimal UnitPrice { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "Units in stock must be non-negative")]
    public short UnitsInStock { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "Units on order must be non-negative")]
    public short UnitsOnOrder { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "Reorder level must be non-negative")]
    public short ReorderLevel { get; set; }

    public bool Discontinued { get; set; }
}
