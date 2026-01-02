namespace EcommerceApp.Domain.Entities;

/// <summary>
/// Represents a city
/// </summary>
public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ProvinceId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    public virtual Province Province { get; set; } = null!;
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
