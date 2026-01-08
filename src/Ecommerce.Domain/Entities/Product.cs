namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents a product in the e-commerce system
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    // Foreign Keys
    public int SupplierId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }

    // Navigation Properties
    public virtual Supplier? Supplier { get; set; }
    public virtual Brand? Brand { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
