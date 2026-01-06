namespace Ecommerce.Domain.Entities;

public class Province
{
    public int Id { get; set; }
    public string ProvinceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
