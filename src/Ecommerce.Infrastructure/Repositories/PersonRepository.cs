using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(EcommerceDbContext context, ILogger<GenericRepository<Person>> logger)
        : base(context, logger)
    {
    }

    public async Task<Person?> GetByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting person with DNI: {Dni}", dni);
            return await _context.Persons
                .FirstOrDefaultAsync(p => p.Dni == dni && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting person with DNI: {Dni}", dni);
            throw;
        }
    }

    public async Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting person with email: {Email}", email);
            return await _context.Persons
                .FirstOrDefaultAsync(p => p.Email == email && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting person with email: {Email}", email);
            throw;
        }
    }

    public async Task<Person?> AuthenticateAsync(string dni, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Authenticating user with DNI: {Dni}", dni);
            var person = await GetByDniAsync(dni, cancellationToken);
            if (person != null && VerifyPassword(password, person.PasswordHash))
            {
                _logger.LogInformation("User authenticated successfully");
                return person;
            }
            _logger.LogWarning("Authentication failed for DNI: {Dni}", dni);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error authenticating user with DNI: {Dni}", dni);
            throw;
        }
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return password == storedHash;
    }
}
