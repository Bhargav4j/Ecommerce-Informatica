namespace EcommerceInformatica.Application.DTOs;

public class BrandUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
