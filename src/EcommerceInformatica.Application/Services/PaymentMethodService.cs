using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PaymentMethodService> _logger;

    public PaymentMethodService(
        IPaymentMethodRepository repository,
        IMapper mapper,
        ILogger<PaymentMethodService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<PaymentMethodDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all payment methods");
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(entities);
            _logger.LogInformation("Successfully retrieved {Count} payment methods", dtos.Count());
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all payment methods");
            throw;
        }
    }

    public async Task<PaymentMethodDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting payment method with ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Payment method with ID: {Id} not found", id);
                return null;
            }

            var dto = _mapper.Map<PaymentMethodDto>(entity);
            _logger.LogInformation("Successfully retrieved payment method with ID: {Id}", id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting payment method with ID: {Id}", id);
            throw;
        }
    }

    public async Task<PaymentMethodDto> CreateAsync(PaymentMethodCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            _logger.LogInformation("Creating new payment method: {Name}", createDto.Name);

            var entity = _mapper.Map<PaymentMethod>(createDto);
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System"; // TODO: Get from current user context
            entity.IsActive = true;

            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<PaymentMethodDto>(createdEntity);

            _logger.LogInformation("Successfully created payment method with ID: {Id}", createdEntity.Id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating payment method: {Name}", createDto?.Name);
            throw;
        }
    }

    public async Task<PaymentMethodDto> UpdateAsync(int id, PaymentMethodUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            _logger.LogInformation("Updating payment method with ID: {Id}", id);

            var existingEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Payment method with ID: {Id} not found for update", id);
                throw new KeyNotFoundException($"Payment method with ID {id} not found");
            }

            _mapper.Map(updateDto, existingEntity);
            existingEntity.ModifiedDate = DateTime.UtcNow;
            existingEntity.ModifiedBy = "System"; // TODO: Get from current user context

            var updatedEntity = await _repository.UpdateAsync(existingEntity, cancellationToken);
            var dto = _mapper.Map<PaymentMethodDto>(updatedEntity);

            _logger.LogInformation("Successfully updated payment method with ID: {Id}", id);
            return dto;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating payment method with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting payment method with ID: {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Payment method with ID: {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Successfully deleted payment method with ID: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting payment method with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<PaymentMethodDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching payment methods with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(entities);
            _logger.LogInformation("Successfully found {Count} payment methods matching search term: {SearchTerm}",
                dtos.Count(), searchTerm);
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching payment methods with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
