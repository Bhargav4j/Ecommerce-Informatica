using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for Person entity operations
/// </summary>
public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    // Task<Person?> GetByDniAsync(string dni, CancellationToken cancellationToken = default);
    Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default);
    Task UpdateAsync(Person person, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    //     Task<IEnumerable<Person>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    // Task<Person?> AuthenticateAsync(string dni, string password, CancellationToken cancellationToken = default);
}
