namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a product supplier
/// </summary>
public class Supplier
{
    public int Id { get; set; }
    public string SupplierId { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Foreign Keys
    public int? ProvinceId { get; set; }
    public int? CityId { get; set; }

    // Navigation Properties
    public Province? Province { get; set; }
    public City? City { get; set; }
    public ICollection<Product>? Products { get; set; }
}
