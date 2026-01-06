using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Category operations
/// </summary>
public interface ICategoryService
{
    Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object categoryCreateDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, object categoryUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
