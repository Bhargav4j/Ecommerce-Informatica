namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a city
/// </summary>
public class City
{
    public int Id { get; set; }
    public string CityId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Foreign Keys
    public int ProvinceId { get; set; }

    // Navigation Properties
    public Province? Province { get; set; }
    public ICollection<Supplier>? Suppliers { get; set; }
}
