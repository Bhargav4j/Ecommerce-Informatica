using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateAsync(ProductCreateDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, ProductUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
}
