using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<City> AddAsync(City city, CancellationToken cancellationToken = default);
    Task UpdateAsync(City city, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<City>> GetByProvinceAsync(int provinceId, CancellationToken cancellationToken = default);
}
