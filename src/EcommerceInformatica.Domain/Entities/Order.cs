namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Order entity (Factura in original system)
/// </summary>
public class Order
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public DateTime OrderDate { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal TotalAmount { get; set; }
    public required string Status { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public Person? Person { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
