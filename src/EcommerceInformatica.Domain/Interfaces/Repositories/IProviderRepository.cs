using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for Provider entity
/// </summary>
public interface IProviderRepository
{
    Task<IEnumerable<Provider>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Provider?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Provider> AddAsync(Provider provider, CancellationToken cancellationToken = default);
    Task UpdateAsync(Provider provider, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Provider>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
