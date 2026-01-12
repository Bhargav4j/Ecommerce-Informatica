namespace EcommerceInformatica.Application.DTOs;

public class InvoiceUpdateDto
{
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsActive { get; set; }
    public int PersonId { get; set; }
    public int PaymentMethodId { get; set; }
}
