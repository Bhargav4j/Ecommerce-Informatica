namespace EcommerceInformatica.Application.DTOs;

public class InvoiceCreateDto
{
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int PersonId { get; set; }
    public int PaymentMethodId { get; set; }
}
