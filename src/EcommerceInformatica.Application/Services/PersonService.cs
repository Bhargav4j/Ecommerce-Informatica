using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonService> _logger;

    public PersonService(
        IPersonRepository repository,
        IMapper mapper,
        ILogger<PersonService> logger)
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
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<PersonDto>>(entities);
            _logger.LogInformation("Successfully retrieved {Count} persons", dtos.Count());
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all persons");
            throw;
        }
    }

    public async Task<PersonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting person with ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Person with ID: {Id} not found", id);
                return null;
            }

            var dto = _mapper.Map<PersonDto>(entity);
            _logger.LogInformation("Successfully retrieved person with ID: {Id}", id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<PersonDto> CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            _logger.LogInformation("Creating new person: {FirstName} {LastName}", createDto.FirstName, createDto.LastName);

            var entity = _mapper.Map<Person>(createDto);
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System"; // TODO: Get from current user context
            entity.IsActive = true;

            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<PersonDto>(createdEntity);

            _logger.LogInformation("Successfully created person with ID: {Id}", createdEntity.Id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating person: {FirstName} {LastName}", createDto?.FirstName, createDto?.LastName);
            throw;
        }
    }

    public async Task<PersonDto> UpdateAsync(int id, PersonUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            _logger.LogInformation("Updating person with ID: {Id}", id);

            var existingEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Person with ID: {Id} not found for update", id);
                throw new KeyNotFoundException($"Person with ID {id} not found");
            }

            _mapper.Map(updateDto, existingEntity);
            existingEntity.ModifiedDate = DateTime.UtcNow;
            existingEntity.ModifiedBy = "System"; // TODO: Get from current user context

            var updatedEntity = await _repository.UpdateAsync(existingEntity, cancellationToken);
            var dto = _mapper.Map<PersonDto>(updatedEntity);

            _logger.LogInformation("Successfully updated person with ID: {Id}", id);
            return dto;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting person with ID: {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Person with ID: {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Successfully deleted person with ID: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting person with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<PersonDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching persons with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<PersonDto>>(entities);
            _logger.LogInformation("Successfully found {Count} persons matching search term: {SearchTerm}", dtos.Count(), searchTerm);
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching persons with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
