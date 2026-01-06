using AutoMapper;
using Microsoft.Extensions.Logging;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository repository, IMapper mapper, ILogger<PersonService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all persons");
            var persons = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all persons");
            throw;
        }
    }

    public async Task<PersonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting person with ID: {Id}", id);
            var person = await _repository.GetByIdAsync(id, cancellationToken);
            return person == null ? null : _mapper.Map<PersonDto>(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<PersonDto> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new person");
            var person = _mapper.Map<Person>(dto);
            var createdPerson = await _repository.AddAsync(person, cancellationToken);
            _logger.LogInformation("Person created successfully with ID: {Id}", createdPerson.Id);
            return _mapper.Map<PersonDto>(createdPerson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating person");
            throw;
        }
    }

    public async Task UpdateAsync(int id, PersonUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating person with ID: {Id}", id);
            var existingPerson = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingPerson == null)
            {
                throw new KeyNotFoundException($"Person with ID {id} not found");
            }

            _mapper.Map(dto, existingPerson);
            await _repository.UpdateAsync(existingPerson, cancellationToken);
            _logger.LogInformation("Person updated successfully with ID: {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating person with ID: {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting person with ID: {Id}", id);
            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Person deleted successfully with ID: {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<PersonDto?> AuthenticateAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Authenticating user with DNI: {Dni}", dto.Dni);
            var person = await _repository.AuthenticateAsync(dto.Dni, dto.Password, cancellationToken);
            if (person == null)
            {
                _logger.LogWarning("Authentication failed for DNI: {Dni}", dto.Dni);
                return null;
            }
            _logger.LogInformation("User authenticated successfully");
            return _mapper.Map<PersonDto>(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error authenticating user");
            throw;
        }
    }
}
