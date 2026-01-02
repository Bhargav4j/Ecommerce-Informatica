namespace EcommerceApp.Domain.Entities;

/// <summary>
/// Represents a product supplier
/// </summary>
public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public int CityId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    public virtual City City { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
