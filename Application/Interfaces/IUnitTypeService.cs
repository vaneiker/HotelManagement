using HotelManagement.Application.DTOs.UnitTypes;

namespace HotelManagement.Application.Interfaces;

public interface IUnitTypeService
{
    Task<IEnumerable<UnitTypeDto>> GetAllAsync(int propertyId);

    Task<UnitTypeDto?> GetByIdAsync(
        int propertyId,
        int unitTypeId);

    Task<int> CreateAsync(
        int propertyId,
        CreateUnitTypeRequest request);

    Task<bool> UpdateAsync(
        int propertyId,
        int unitTypeId,
        UpdateUnitTypeRequest request);

    Task<bool> DeleteAsync(
        int propertyId,
        int unitTypeId);
}