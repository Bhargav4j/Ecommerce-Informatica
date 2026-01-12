namespace EcommerceInformatica.Application.DTOs;

public class InvoiceDetailCreateDto
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
}
