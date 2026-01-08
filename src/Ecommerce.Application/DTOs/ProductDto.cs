namespace Ecommerce.Application.DTOs;

/// <summary>
/// Data Transfer Object for Product
/// </summary>
public class ProductDto
{
    public int Id { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public int BrandId { get; set; }
    public string? BrandName { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}

/// <summary>
/// DTO for creating a new Product
/// </summary>
public class ProductCreateDto
{
    public string ProductCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public int SupplierId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
}

/// <summary>
/// DTO for updating an existing Product
/// </summary>
public class ProductUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public int SupplierId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
}
