using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface ICityService
{
    Task<IEnumerable<CityDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CityDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CityDto> CreateAsync(CityCreateDto createDto, CancellationToken cancellationToken = default);
    Task<CityDto> UpdateAsync(int id, CityUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CityDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
