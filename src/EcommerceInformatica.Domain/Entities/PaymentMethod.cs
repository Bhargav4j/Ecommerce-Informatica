namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// PaymentMethod entity for order payment methods
/// </summary>
public class PaymentMethod
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
