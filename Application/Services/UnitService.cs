using HotelManagement.Application.DTOs.Units;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<IEnumerable<UnitDto>> GetAllAsync(
        int propertyId)
    {
        var units = await _unitRepository
            .GetAllAsync(propertyId);

        return units.Select(MapToDto);
    }

    public async Task<UnitDto?> GetByIdAsync(
        int propertyId,
        int unitId)
    {
        var unit = await _unitRepository
            .GetByIdAsync(propertyId, unitId);

        if (unit == null)
            return null;

        return MapToDto(unit);
    }

    public async Task<int> CreateAsync(
        int propertyId,
        CreateUnitRequest request)
    {
        var unit = new Unit
        {
            PropertyId = propertyId,
            UnitTypeId = request.UnitTypeId,
            Code = request.Code,
            Name = request.Name,
            FloorNumber = request.FloorNumber,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _unitRepository.CreateAsync(unit);
    }

    public async Task<bool> UpdateAsync(
        int propertyId,
        int unitId,
        UpdateUnitRequest request)
    {
        var unit = await _unitRepository
            .GetByIdAsync(propertyId, unitId);

        if (unit == null)
            return false;

        unit.UnitTypeId = request.UnitTypeId;
        unit.Code = request.Code;
        unit.Name = request.Name;
        unit.FloorNumber = request.FloorNumber;
        unit.Description = request.Description;
        unit.IsActive = request.IsActive;
        unit.UpdatedAt = DateTime.UtcNow;

        return await _unitRepository.UpdateAsync(unit);
    }

    public async Task<bool> DeleteAsync(
        int propertyId,
        int unitId)
    {
        return await _unitRepository
            .DeleteAsync(propertyId, unitId);
    }

    private static UnitDto MapToDto(Unit unit)
    {
        return new UnitDto
        {
            UnitId = unit.UnitId,
            PropertyId = unit.PropertyId,
            UnitTypeId = unit.UnitTypeId,
            Code = unit.Code,
            Name = unit.Name,
            FloorNumber = unit.FloorNumber,
            Description = unit.Description,
            IsActive = unit.IsActive
        };
    }
}