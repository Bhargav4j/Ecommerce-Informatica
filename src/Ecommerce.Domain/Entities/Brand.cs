namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a product brand
/// </summary>
public class Brand
{
    public int Id { get; set; }
    public string BrandId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Navigation Properties
    public ICollection<Product>? Products { get; set; }
}
