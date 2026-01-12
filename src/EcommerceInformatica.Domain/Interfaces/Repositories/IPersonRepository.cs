using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Person> AddAsync(Person entity, CancellationToken cancellationToken = default);
    Task<Person> UpdateAsync(Person entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Person>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
