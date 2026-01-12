using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Supplier> AddAsync(Supplier entity, CancellationToken cancellationToken = default);
    Task<Supplier> UpdateAsync(Supplier entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Supplier>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
