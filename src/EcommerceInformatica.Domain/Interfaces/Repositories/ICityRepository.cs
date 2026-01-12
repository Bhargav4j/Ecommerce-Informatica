using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<City> AddAsync(City entity, CancellationToken cancellationToken = default);
    Task<City> UpdateAsync(City entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<City>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
