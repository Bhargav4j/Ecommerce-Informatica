namespace EcommerceInformatica.Domain.Entities;

public class Invoice
{
    public int Id { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    public int PersonId { get; set; }
    public Person? Person { get; set; }

    public int PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

    public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
