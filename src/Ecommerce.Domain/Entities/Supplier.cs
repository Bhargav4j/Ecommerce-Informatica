namespace Ecommerce.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string SupplierId { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int ProvinceId { get; set; }
    public int CityId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    public virtual Province? Province { get; set; }
    public virtual City? City { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
