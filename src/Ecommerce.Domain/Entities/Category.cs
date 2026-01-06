namespace Ecommerce.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
