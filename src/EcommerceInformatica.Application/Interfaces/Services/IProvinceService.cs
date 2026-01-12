using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IProvinceService
{
    Task<IEnumerable<ProvinceDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProvinceDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProvinceDto> CreateAsync(ProvinceCreateDto createDto, CancellationToken cancellationToken = default);
    Task<ProvinceDto> UpdateAsync(int id, ProvinceUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProvinceDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
