namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents an invoice in the system
/// </summary>
public class Invoice
{
    public int Id { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Foreign Keys
    public int PersonId { get; set; }
    public int PaymentMethodId { get; set; }

    // Navigation Properties
    public Person? Person { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public ICollection<InvoiceDetail>? InvoiceDetails { get; set; }
}
