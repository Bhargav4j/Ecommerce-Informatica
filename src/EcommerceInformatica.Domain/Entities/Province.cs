namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Province entity for address information
/// </summary>
public class Province
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public ICollection<City> Cities { get; set; } = new List<City>();
    public ICollection<Person> Persons { get; set; } = new List<Person>();
}
