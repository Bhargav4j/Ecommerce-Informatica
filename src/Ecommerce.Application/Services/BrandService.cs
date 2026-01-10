using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<BrandService> _logger;

    public BrandService(
        IBrandRepository brandRepository,
        IMapper mapper,
        ILogger<BrandService> logger)
    {
        _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting brand with ID: {BrandId}", id);

            var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);
            if (brand == null)
            {
                _logger.LogWarning("Brand with ID: {BrandId} not found", id);
                return null;
            }

            return _mapper.Map<BrandDto>(brand);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting brand with ID: {BrandId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<BrandDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all brands");

            var brands = await _brandRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all brands");
            throw;
        }
    }

    public async Task<BrandDto> CreateAsync(BrandCreateDto brandCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new brand: {BrandName}", brandCreateDto.BrandName);

            var brand = _mapper.Map<Brand>(brandCreateDto);
            var createdBrand = await _brandRepository.AddAsync(brand, cancellationToken);

            _logger.LogInformation("Successfully created brand with ID: {BrandId}", createdBrand.BrandId);

            return _mapper.Map<BrandDto>(createdBrand);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating brand: {BrandName}", brandCreateDto.BrandName);
            throw;
        }
    }

    public async Task<BrandDto?> UpdateAsync(BrandUpdateDto brandUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating brand with ID: {BrandId}", brandUpdateDto.BrandId);

            var existingBrand = await _brandRepository.GetByIdAsync(brandUpdateDto.BrandId, cancellationToken);
            if (existingBrand == null)
            {
                _logger.LogWarning("Brand with ID: {BrandId} not found for update", brandUpdateDto.BrandId);
                return null;
            }

            _mapper.Map(brandUpdateDto, existingBrand);
            await _brandRepository.UpdateAsync(existingBrand, cancellationToken);

            _logger.LogInformation("Successfully updated brand with ID: {BrandId}", brandUpdateDto.BrandId);

            return _mapper.Map<BrandDto>(existingBrand);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating brand with ID: {BrandId}", brandUpdateDto.BrandId);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting brand with ID: {BrandId}", id);

            if (!await _brandRepository.ExistsAsync(id, cancellationToken))
            {
                _logger.LogWarning("Brand with ID: {BrandId} not found for deletion", id);
                return false;
            }

            await _brandRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Successfully deleted brand with ID: {BrandId}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting brand with ID: {BrandId}", id);
            throw;
        }
    }
}
