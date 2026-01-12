using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Invoice> AddAsync(Invoice entity, CancellationToken cancellationToken = default);
    Task<Invoice> UpdateAsync(Invoice entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Invoice>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
