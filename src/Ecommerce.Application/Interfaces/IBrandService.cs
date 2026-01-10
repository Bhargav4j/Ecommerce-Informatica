using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces;

/// <summary>
/// Service interface for Brand business operations
/// </summary>
public interface IBrandService
{
    Task<IEnumerable<BrandDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BrandDto> CreateAsync(BrandCreateDto dto, CancellationToken cancellationToken = default);
    Task<BrandDto?> UpdateAsync(BrandUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
