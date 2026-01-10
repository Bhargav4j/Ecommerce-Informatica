using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(EcommerceDbContext context, ILogger<PersonRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving person with ID: {Id}", id);

            return await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all persons");

            return await _context.Persons
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all persons");
            throw;
        }
    }

    public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new person: {FirstName} {LastName}", person.FirstName, person.LastName);

            await _context.Persons.AddAsync(person, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully added person with ID: {Id}", person.Id);

            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding person: {FirstName} {LastName}", person.FirstName, person.LastName);
            throw;
        }
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating person with ID: {Id}", person.Id);

            _context.Persons.Update(person);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated person with ID: {Id}", person.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating person with ID: {Id}", person.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting person with ID: {Id}", id);

            var person = await _context.Persons.FindAsync(new object[] { id }, cancellationToken);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted person with ID: {Id}", id);
            }
            else
            {
                _logger.LogWarning("Person with ID: {Id} not found for deletion", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons.AnyAsync(p => p.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of person with ID: {Id}", id);
            throw;
        }
    }
}
