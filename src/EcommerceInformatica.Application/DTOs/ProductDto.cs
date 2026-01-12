namespace EcommerceInformatica.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int BrandId { get; set; }
    public string? BrandName { get; set; }
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
}
