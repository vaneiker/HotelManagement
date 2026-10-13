using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces;

public interface IUnitTypeRepository
{
    Task<IEnumerable<UnitType>> GetAllAsync(int propertyId);

    Task<UnitType?> GetByIdAsync(
        int propertyId,
        int unitTypeId);

    Task<int> CreateAsync(UnitType unitType);

    Task<bool> UpdateAsync(UnitType unitType);

    Task<bool> DeleteAsync(
        int propertyId,
        int unitTypeId);
}