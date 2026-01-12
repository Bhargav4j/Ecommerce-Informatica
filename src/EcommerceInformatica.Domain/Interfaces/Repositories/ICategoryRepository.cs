using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Category> AddAsync(Category entity, CancellationToken cancellationToken = default);
    Task<Category> UpdateAsync(Category entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
