namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Product entity representing items available in the e-commerce store
/// </summary>
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public Provider? Provider { get; set; }
    public Brand? Brand { get; set; }
    public Category? Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
