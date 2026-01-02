using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CategoryDto> CreateAsync(CategoryCreateDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CategoryUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CategoryDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
