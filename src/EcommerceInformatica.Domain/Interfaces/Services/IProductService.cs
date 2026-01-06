using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Product operations
/// </summary>
public interface IProductService
{
    Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object productCreateDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, object productUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<object>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
}
