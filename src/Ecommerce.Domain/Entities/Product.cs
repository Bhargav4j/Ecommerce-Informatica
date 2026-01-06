namespace Ecommerce.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    public virtual Supplier? Supplier { get; set; }
    public virtual Brand? Brand { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
