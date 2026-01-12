using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

public class InvoiceDetailService : IInvoiceDetailService
{
    private readonly IInvoiceDetailRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<InvoiceDetailService> _logger;

    public InvoiceDetailService(
        IInvoiceDetailRepository repository,
        IMapper mapper,
        ILogger<InvoiceDetailService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<InvoiceDetailDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all invoice details");
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<InvoiceDetailDto>>(entities);
            _logger.LogInformation("Successfully retrieved {Count} invoice details", dtos.Count());
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all invoice details");
            throw;
        }
    }

    public async Task<InvoiceDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoice detail with ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Invoice detail with ID: {Id} not found", id);
                return null;
            }

            var dto = _mapper.Map<InvoiceDetailDto>(entity);
            _logger.LogInformation("Successfully retrieved invoice detail with ID: {Id}", id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting invoice detail with ID: {Id}", id);
            throw;
        }
    }

    public async Task<InvoiceDetailDto> CreateAsync(InvoiceDetailCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            _logger.LogInformation("Creating new invoice detail for invoice ID: {InvoiceId}, product ID: {ProductId}",
                createDto.InvoiceId, createDto.ProductId);

            var entity = _mapper.Map<InvoiceDetail>(createDto);
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System"; // TODO: Get from current user context
            entity.IsActive = true;

            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<InvoiceDetailDto>(createdEntity);

            _logger.LogInformation("Successfully created invoice detail with ID: {Id}", createdEntity.Id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating invoice detail for invoice ID: {InvoiceId}",
                createDto?.InvoiceId);
            throw;
        }
    }

    public async Task<InvoiceDetailDto> UpdateAsync(int id, InvoiceDetailUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            _logger.LogInformation("Updating invoice detail with ID: {Id}", id);

            var existingEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Invoice detail with ID: {Id} not found for update", id);
                throw new KeyNotFoundException($"Invoice detail with ID {id} not found");
            }

            _mapper.Map(updateDto, existingEntity);
            existingEntity.ModifiedDate = DateTime.UtcNow;
            existingEntity.ModifiedBy = "System"; // TODO: Get from current user context

            var updatedEntity = await _repository.UpdateAsync(existingEntity, cancellationToken);
            var dto = _mapper.Map<InvoiceDetailDto>(updatedEntity);

            _logger.LogInformation("Successfully updated invoice detail with ID: {Id}", id);
            return dto;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating invoice detail with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting invoice detail with ID: {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Invoice detail with ID: {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Successfully deleted invoice detail with ID: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting invoice detail with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<InvoiceDetailDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching invoice details with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<InvoiceDetailDto>>(entities);
            _logger.LogInformation("Successfully found {Count} invoice details matching search term: {SearchTerm}",
                dtos.Count(), searchTerm);
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching invoice details with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
