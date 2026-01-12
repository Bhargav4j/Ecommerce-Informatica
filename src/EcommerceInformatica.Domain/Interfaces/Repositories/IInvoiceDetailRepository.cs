using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface IInvoiceDetailRepository
{
    Task<IEnumerable<InvoiceDetail>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InvoiceDetail?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<InvoiceDetail> AddAsync(InvoiceDetail entity, CancellationToken cancellationToken = default);
    Task<InvoiceDetail> UpdateAsync(InvoiceDetail entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<InvoiceDetail>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
