using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SupplierDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<SupplierDto> CreateAsync(SupplierCreateDto createDto, CancellationToken cancellationToken = default);
    Task<SupplierDto> UpdateAsync(int id, SupplierUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SupplierDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
