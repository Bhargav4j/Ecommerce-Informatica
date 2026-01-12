using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface IPaymentMethodRepository
{
    Task<IEnumerable<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PaymentMethod> AddAsync(PaymentMethod entity, CancellationToken cancellationToken = default);
    Task<PaymentMethod> UpdateAsync(PaymentMethod entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PaymentMethod>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
