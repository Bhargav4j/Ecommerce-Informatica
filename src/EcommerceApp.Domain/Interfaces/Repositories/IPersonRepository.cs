using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Person?> GetByDniAsync(string dni, CancellationToken cancellationToken = default);
    Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default);
    Task UpdateAsync(Person person, CancellationToken cancellationToken = default);
    Task DeleteAsync(string dni, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string dni, CancellationToken cancellationToken = default);
    Task<Person?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
}
