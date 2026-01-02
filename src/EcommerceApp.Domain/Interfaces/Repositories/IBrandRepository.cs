using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Brand> AddAsync(Brand brand, CancellationToken cancellationToken = default);
    Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
