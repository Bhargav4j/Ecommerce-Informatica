namespace EcommerceInformatica.Application.DTOs;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public int SupplierId { get; set; }
}
