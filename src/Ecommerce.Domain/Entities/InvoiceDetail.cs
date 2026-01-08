namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents invoice detail items
/// </summary>
public class InvoiceDetail
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime CreatedDate { get; set; }

    // Foreign Keys
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }

    // Navigation Properties
    public virtual Invoice? Invoice { get; set; }
    public virtual Product? Product { get; set; }
}
