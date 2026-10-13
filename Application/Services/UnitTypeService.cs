using HotelManagement.Application.DTOs.UnitTypes;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Services;

public class UnitTypeService : IUnitTypeService
{
    private readonly IUnitTypeRepository _unitTypeRepository;

    public UnitTypeService(IUnitTypeRepository unitTypeRepository)
    {
        _unitTypeRepository = unitTypeRepository;
    }

    public async Task<IEnumerable<UnitTypeDto>> GetAllAsync(
        int propertyId)
    {
        var unitTypes = await _unitTypeRepository
            .GetAllAsync(propertyId);

        return unitTypes.Select(MapToDto);
    }

    public async Task<UnitTypeDto?> GetByIdAsync(
        int propertyId,
        int unitTypeId)
    {
        var unitType = await _unitTypeRepository
            .GetByIdAsync(propertyId, unitTypeId);

        if (unitType == null)
            return null;

        return MapToDto(unitType);
    }

    public async Task<int> CreateAsync(
        int propertyId,
        CreateUnitTypeRequest request)
    {
        var unitType = new UnitType
        {
            PropertyId = propertyId,
            Name = request.Name,
            Description = request.Description,
            MaxAdults = request.MaxAdults,
            MaxChildren = request.MaxChildren,
            BaseCapacity = request.BaseCapacity,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _unitTypeRepository.CreateAsync(unitType);
    }

    public async Task<bool> UpdateAsync(
        int propertyId,
        int unitTypeId,
        UpdateUnitTypeRequest request)
    {
        var unitType = await _unitTypeRepository
            .GetByIdAsync(propertyId, unitTypeId);

        if (unitType == null)
            return false;

        unitType.Name = request.Name;
        unitType.Description = request.Description;
        unitType.MaxAdults = request.MaxAdults;
        unitType.MaxChildren = request.MaxChildren;
        unitType.BaseCapacity = request.BaseCapacity;
        unitType.IsActive = request.IsActive;
        unitType.UpdatedAt = DateTime.UtcNow;

        return await _unitTypeRepository.UpdateAsync(unitType);
    }

    public async Task<bool> DeleteAsync(
        int propertyId,
        int unitTypeId)
    {
        return await _unitTypeRepository
            .DeleteAsync(propertyId, unitTypeId);
    }

    private static UnitTypeDto MapToDto(UnitType unitType)
    {
        return new UnitTypeDto
        {
            UnitTypeId = unitType.UnitTypeId,
            PropertyId = unitType.PropertyId,
            Name = unitType.Name,
            Description = unitType.Description,
            MaxAdults = unitType.MaxAdults,
            MaxChildren = unitType.MaxChildren,
            BaseCapacity = unitType.BaseCapacity,
            IsActive = unitType.IsActive
        };
    }
}