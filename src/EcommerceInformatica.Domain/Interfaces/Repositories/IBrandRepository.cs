using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Brand> AddAsync(Brand entity, CancellationToken cancellationToken = default);
    Task<Brand> UpdateAsync(Brand entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
