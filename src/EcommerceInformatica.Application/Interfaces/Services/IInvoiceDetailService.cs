using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IInvoiceDetailService
{
    Task<IEnumerable<InvoiceDetailDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InvoiceDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<InvoiceDetailDto> CreateAsync(InvoiceDetailCreateDto createDto, CancellationToken cancellationToken = default);
    Task<InvoiceDetailDto> UpdateAsync(int id, InvoiceDetailUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<InvoiceDetailDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
