namespace EcommerceInformatica.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
