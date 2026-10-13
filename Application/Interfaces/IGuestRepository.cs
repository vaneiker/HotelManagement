using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces;

public interface IGuestRepository
{
    Task<IEnumerable<Guest>> GetAllAsync(int companyId);

    Task<Guest?> GetByIdAsync(
        int companyId,
        long guestId);

    Task<long> CreateAsync(Guest guest);

    Task<bool> UpdateAsync(Guest guest);

    Task<bool> DeleteAsync(
        int companyId,
        long guestId);
}