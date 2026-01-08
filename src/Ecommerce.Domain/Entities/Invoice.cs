namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents an invoice in the e-commerce system
/// </summary>
public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Foreign Keys
    public int CustomerId { get; set; }
    public int PaymentMethodId { get; set; }

    // Navigation Properties
    public virtual Customer? Customer { get; set; }
    public virtual PaymentMethod? PaymentMethod { get; set; }
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
