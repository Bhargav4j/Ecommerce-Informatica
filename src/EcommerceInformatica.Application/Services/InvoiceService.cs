using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(
        IInvoiceRepository repository,
        IMapper mapper,
        ILogger<InvoiceService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<InvoiceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all invoices");
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<InvoiceDto>>(entities);
            _logger.LogInformation("Successfully retrieved {Count} invoices", dtos.Count());
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all invoices");
            throw;
        }
    }

    public async Task<InvoiceDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoice with ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Invoice with ID: {Id} not found", id);
                return null;
            }

            var dto = _mapper.Map<InvoiceDto>(entity);
            _logger.LogInformation("Successfully retrieved invoice with ID: {Id}", id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting invoice with ID: {Id}", id);
            throw;
        }
    }

    public async Task<InvoiceDto> CreateAsync(InvoiceCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            _logger.LogInformation("Creating new invoice for person ID: {PersonId}", createDto.PersonId);

            var entity = _mapper.Map<Invoice>(createDto);
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System"; // TODO: Get from current user context
            entity.IsActive = true;

            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<InvoiceDto>(createdEntity);

            _logger.LogInformation("Successfully created invoice with ID: {Id}", createdEntity.Id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating invoice for person ID: {PersonId}", createDto?.PersonId);
            throw;
        }
    }

    public async Task<InvoiceDto> UpdateAsync(int id, InvoiceUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            _logger.LogInformation("Updating invoice with ID: {Id}", id);

            var existingEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Invoice with ID: {Id} not found for update", id);
                throw new KeyNotFoundException($"Invoice with ID {id} not found");
            }

            _mapper.Map(updateDto, existingEntity);
            existingEntity.ModifiedDate = DateTime.UtcNow;
            existingEntity.ModifiedBy = "System"; // TODO: Get from current user context

            var updatedEntity = await _repository.UpdateAsync(existingEntity, cancellationToken);
            var dto = _mapper.Map<InvoiceDto>(updatedEntity);

            _logger.LogInformation("Successfully updated invoice with ID: {Id}", id);
            return dto;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating invoice with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting invoice with ID: {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Invoice with ID: {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Successfully deleted invoice with ID: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting invoice with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<InvoiceDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching invoices with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<InvoiceDto>>(entities);
            _logger.LogInformation("Successfully found {Count} invoices matching search term: {SearchTerm}", dtos.Count(), searchTerm);
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching invoices with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
