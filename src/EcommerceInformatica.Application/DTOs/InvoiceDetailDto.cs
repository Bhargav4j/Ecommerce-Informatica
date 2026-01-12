namespace EcommerceInformatica.Application.DTOs;

public class InvoiceDetailDto
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
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
}
