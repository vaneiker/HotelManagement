using HotelManagement.Application.DTOs.Guests;

namespace HotelManagement.Application.Interfaces;

public interface IGuestService
{
    Task<IEnumerable<GuestDto>> GetAllAsync(int companyId);

    Task<GuestDto?> GetByIdAsync(
        int companyId,
        long guestId);

    Task<long> CreateAsync(
        int companyId,
        CreateGuestRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        long guestId,
        UpdateGuestRequest request);

    Task<bool> DeleteAsync(
        int companyId,
        long guestId);
}