namespace EcommerceInformatica.Application.DTOs;

public class CityCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProvinceId { get; set; }
}
