namespace EcommerceInformatica.Application.DTOs;

public class InvoiceDto
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
    public string? PersonName { get; set; }
    public int PaymentMethodId { get; set; }
    public string? PaymentMethodName { get; set; }
}
