namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Represents a city
/// </summary>
public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    // Foreign keys
    public int ProvinceId { get; set; }

    // Navigation properties
    public virtual Province Province { get; set; } = null!;
    public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
}
