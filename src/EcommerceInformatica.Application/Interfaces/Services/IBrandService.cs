using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IBrandService
{
    Task<IEnumerable<BrandDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BrandDto> CreateAsync(BrandCreateDto createDto, CancellationToken cancellationToken = default);
    Task<BrandDto> UpdateAsync(int id, BrandUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BrandDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
