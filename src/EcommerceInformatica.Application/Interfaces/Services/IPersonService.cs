using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PersonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PersonDto> CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken = default);
    Task<PersonDto> UpdateAsync(int id, PersonUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PersonDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
