using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface IProvinceRepository
{
    Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Province> AddAsync(Province entity, CancellationToken cancellationToken = default);
    Task<Province> UpdateAsync(Province entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Province>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
