using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PersonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PersonDto> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, PersonUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<PersonDto?> AuthenticateAsync(LoginDto dto, CancellationToken cancellationToken = default);
}
