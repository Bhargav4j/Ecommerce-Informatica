using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProvinceRepository
{
    Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Province> AddAsync(Province province, CancellationToken cancellationToken = default);
    Task UpdateAsync(Province province, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
