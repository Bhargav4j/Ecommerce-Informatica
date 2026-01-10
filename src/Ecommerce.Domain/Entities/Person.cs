namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a person (customer or admin) in the system
/// </summary>
public class Person
{
    public int Id { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Navigation Properties
    public ICollection<Invoice>? Invoices { get; set; }
}
