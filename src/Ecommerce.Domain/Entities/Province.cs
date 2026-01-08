namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a province
/// </summary>
public class Province
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    // Navigation Properties
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
