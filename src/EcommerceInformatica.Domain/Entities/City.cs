namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// City entity for address information
/// </summary>
public class City
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ProvinceId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public Province? Province { get; set; }
    public ICollection<Person> Persons { get; set; } = new List<Person>();
}
