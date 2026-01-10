using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces;

/// <summary>
/// Service interface for Person business operations
/// </summary>
public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PersonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PersonDto> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken = default);
    Task<PersonDto?> UpdateAsync(PersonUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
