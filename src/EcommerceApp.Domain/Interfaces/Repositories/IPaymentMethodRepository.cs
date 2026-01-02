using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IPaymentMethodRepository
{
    Task<IEnumerable<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PaymentMethod> AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
    Task UpdateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
