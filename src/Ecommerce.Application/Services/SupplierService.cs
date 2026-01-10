using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(
        ISupplierRepository supplierRepository,
        IMapper mapper,
        ILogger<SupplierService> logger)
    {
        _supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<SupplierDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting supplier with ID: {SupplierId}", id);

            var supplier = await _supplierRepository.GetByIdAsync(id, cancellationToken);
            if (supplier == null)
            {
                _logger.LogWarning("Supplier with ID: {SupplierId} not found", id);
                return null;
            }

            return _mapper.Map<SupplierDto>(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting supplier with ID: {SupplierId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all suppliers");

            var suppliers = await _supplierRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all suppliers");
            throw;
        }
    }

    public async Task<SupplierDto> CreateAsync(SupplierCreateDto supplierCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new supplier: {CompanyName}", supplierCreateDto.CompanyName);

            var supplier = _mapper.Map<Supplier>(supplierCreateDto);
            var createdSupplier = await _supplierRepository.AddAsync(supplier, cancellationToken);

            _logger.LogInformation("Successfully created supplier with ID: {SupplierId}", createdSupplier.SupplierId);

            return _mapper.Map<SupplierDto>(createdSupplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating supplier: {CompanyName}", supplierCreateDto.CompanyName);
            throw;
        }
    }

    public async Task<SupplierDto?> UpdateAsync(SupplierUpdateDto supplierUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating supplier with ID: {SupplierId}", supplierUpdateDto.SupplierId);

            var existingSupplier = await _supplierRepository.GetByIdAsync(supplierUpdateDto.SupplierId, cancellationToken);
            if (existingSupplier == null)
            {
                _logger.LogWarning("Supplier with ID: {SupplierId} not found for update", supplierUpdateDto.SupplierId);
                return null;
            }

            _mapper.Map(supplierUpdateDto, existingSupplier);
            await _supplierRepository.UpdateAsync(existingSupplier, cancellationToken);

            _logger.LogInformation("Successfully updated supplier with ID: {SupplierId}", supplierUpdateDto.SupplierId);

            return _mapper.Map<SupplierDto>(existingSupplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating supplier with ID: {SupplierId}", supplierUpdateDto.SupplierId);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting supplier with ID: {SupplierId}", id);

            if (!await _supplierRepository.ExistsAsync(id, cancellationToken))
            {
                _logger.LogWarning("Supplier with ID: {SupplierId} not found for deletion", id);
                return false;
            }

            await _supplierRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Successfully deleted supplier with ID: {SupplierId}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting supplier with ID: {SupplierId}", id);
            throw;
        }
    }
}
