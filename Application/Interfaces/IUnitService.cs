using HotelManagement.Application.DTOs.Units;

namespace HotelManagement.Application.Interfaces;

public interface IUnitService
{
    Task<IEnumerable<UnitDto>> GetAllAsync(int propertyId);

    Task<UnitDto?> GetByIdAsync(
        int propertyId,
        int unitId);

    Task<int> CreateAsync(
        int propertyId,
        CreateUnitRequest request);

    Task<bool> UpdateAsync(
        int propertyId,
        int unitId,
        UpdateUnitRequest request);

    Task<bool> DeleteAsync(
        int propertyId,
        int unitId);
}