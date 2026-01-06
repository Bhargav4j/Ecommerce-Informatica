namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Represents a province
/// </summary>
public class Province
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
    public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
}
