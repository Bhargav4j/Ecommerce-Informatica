using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InvoiceDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<InvoiceDto> CreateAsync(InvoiceCreateDto createDto, CancellationToken cancellationToken = default);
    Task<InvoiceDto> UpdateAsync(int id, InvoiceUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<InvoiceDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
