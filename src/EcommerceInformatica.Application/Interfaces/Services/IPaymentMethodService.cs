using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IPaymentMethodService
{
    Task<IEnumerable<PaymentMethodDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentMethodDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PaymentMethodDto> CreateAsync(PaymentMethodCreateDto createDto, CancellationToken cancellationToken = default);
    Task<PaymentMethodDto> UpdateAsync(int id, PaymentMethodUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PaymentMethodDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
