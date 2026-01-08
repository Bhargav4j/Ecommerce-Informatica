namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a customer/user in the e-commerce system
/// </summary>
public class Customer
{
    public int Id { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Navigation Properties
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
