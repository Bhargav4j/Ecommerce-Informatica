namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a detail line in an invoice
/// </summary>
public class InvoiceDetail
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Foreign Keys
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }

    // Navigation Properties
    public Invoice? Invoice { get; set; }
    public Product? Product { get; set; }
}
