namespace Ecommerce.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public string? SupplierName { get; set; }
    public string? BrandName { get; set; }
    public string? CategoryName { get; set; }
}

public class ProductCreateDto
{
    public string ProductId { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
}

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
}
