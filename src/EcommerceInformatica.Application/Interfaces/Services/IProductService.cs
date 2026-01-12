using EcommerceInformatica.Application.DTOs;

namespace EcommerceInformatica.Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateAsync(ProductCreateDto createDto, CancellationToken cancellationToken = default);
    Task<ProductDto> UpdateAsync(int id, ProductUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
