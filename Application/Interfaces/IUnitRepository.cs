using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces;

public interface IUnitRepository
{
    Task<IEnumerable<Unit>> GetAllAsync(int propertyId);

    Task<Unit?> GetByIdAsync(
        int propertyId,
        int unitId);

    Task<int> CreateAsync(Unit unit);

    Task<bool> UpdateAsync(Unit unit);

    Task<bool> DeleteAsync(
        int propertyId,
        int unitId);
}