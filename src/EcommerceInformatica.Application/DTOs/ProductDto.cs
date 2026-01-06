namespace EcommerceInformatica.Application.DTOs;

/// <summary>
/// Data Transfer Object for Product
/// </summary>
public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public string? ProviderName { get; set; }
    public int BrandId { get; set; }
    public string? BrandName { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for creating a new Product
/// </summary>
public class ProductCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
}

/// <summary>
/// DTO for updating an existing Product
/// </summary>
public class ProductUpdateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
}
