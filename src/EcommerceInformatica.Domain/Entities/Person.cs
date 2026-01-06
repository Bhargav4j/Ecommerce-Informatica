namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Represents a person (customer/user) in the system
/// </summary>
public class Person
{
    public int Id { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Password { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    // Foreign keys
    public int? CityId { get; set; }
    public int? ProvinceId { get; set; }

    // Navigation properties
    public virtual City? City { get; set; }
    public virtual Province? Province { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
