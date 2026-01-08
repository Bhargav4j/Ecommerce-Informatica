namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a city
/// </summary>
public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    // Foreign Keys
    public int ProvinceId { get; set; }

    // Navigation Properties
    public virtual Province? Province { get; set; }
}
