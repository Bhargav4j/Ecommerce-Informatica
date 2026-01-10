using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonService> _logger;

    public PersonService(
        IPersonRepository personRepository,
        IMapper mapper,
        ILogger<PersonService> logger)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PersonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting person with ID: {PersonId}", id);

            var person = await _personRepository.GetByIdAsync(id, cancellationToken);
            if (person == null)
            {
                _logger.LogWarning("Person with ID: {PersonId} not found", id);
                return null;
            }

            return _mapper.Map<PersonDto>(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting person with ID: {PersonId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all persons");

            var persons = await _personRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all persons");
            throw;
        }
    }

    public async Task<PersonDto> CreateAsync(PersonCreateDto personCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new person: {FirstName} {LastName}",
                personCreateDto.FirstName, personCreateDto.LastName);

            var person = _mapper.Map<Person>(personCreateDto);
            var createdPerson = await _personRepository.AddAsync(person, cancellationToken);

            _logger.LogInformation("Successfully created person with ID: {PersonId}", createdPerson.Id);

            return _mapper.Map<PersonDto>(createdPerson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating person: {FirstName} {LastName}",
                personCreateDto.FirstName, personCreateDto.LastName);
            throw;
        }
    }

    public async Task<PersonDto?> UpdateAsync(PersonUpdateDto personUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating person with ID: {PersonId}", personUpdateDto.PersonId);

            var existingPerson = await _personRepository.GetByIdAsync(personUpdateDto.PersonId, cancellationToken);
            if (existingPerson == null)
            {
                _logger.LogWarning("Person with ID: {PersonId} not found for update", personUpdateDto.PersonId);
                return null;
            }

            _mapper.Map(personUpdateDto, existingPerson);
            await _personRepository.UpdateAsync(existingPerson, cancellationToken);

            _logger.LogInformation("Successfully updated person with ID: {PersonId}", personUpdateDto.PersonId);

            return _mapper.Map<PersonDto>(existingPerson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating person with ID: {PersonId}", personUpdateDto.PersonId);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting person with ID: {PersonId}", id);

            if (!await _personRepository.ExistsAsync(id, cancellationToken))
            {
                _logger.LogWarning("Person with ID: {PersonId} not found for deletion", id);
                return false;
            }

            await _personRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Successfully deleted person with ID: {PersonId}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting person with ID: {PersonId}", id);
            throw;
        }
    }
}
