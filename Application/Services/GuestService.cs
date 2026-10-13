using HotelManagement.Application.DTOs.Guests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Services;

public class GuestService : IGuestService
{
    private readonly IGuestRepository _guestRepository;

    public GuestService(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<IEnumerable<GuestDto>> GetAllAsync(int companyId)
    {
        var guests = await _guestRepository.GetAllAsync(companyId);

        return guests.Select(MapToDto);
    }

    public async Task<GuestDto?> GetByIdAsync(
        int companyId,
        long guestId)
    {
        var guest = await _guestRepository.GetByIdAsync(
            companyId,
            guestId);

        if (guest == null)
            return null;

        return MapToDto(guest);
    }

    public async Task<long> CreateAsync(
        int companyId,
        CreateGuestRequest request)
    {
        var guest = new Guest
        {
            CompanyId = companyId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DocumentType = request.DocumentType,
            DocumentNumber = request.DocumentNumber,
            Email = request.Email,
            Phone = request.Phone,
            Nationality = request.Nationality,
            Address = request.Address,
            City = request.City,
            CountryCode = request.CountryCode,
            Notes = request.Notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _guestRepository.CreateAsync(guest);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        long guestId,
        UpdateGuestRequest request)
    {
        var guest = await _guestRepository.GetByIdAsync(
            companyId,
            guestId);

        if (guest == null)
            return false;

        guest.FirstName = request.FirstName;
        guest.LastName = request.LastName;
        guest.DocumentType = request.DocumentType;
        guest.DocumentNumber = request.DocumentNumber;
        guest.Email = request.Email;
        guest.Phone = request.Phone;
        guest.Nationality = request.Nationality;
        guest.Address = request.Address;
        guest.City = request.City;
        guest.CountryCode = request.CountryCode;
        guest.Notes = request.Notes;
        guest.IsActive = request.IsActive;
        guest.UpdatedAt = DateTime.UtcNow;

        return await _guestRepository.UpdateAsync(guest);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long guestId)
    {
        return await _guestRepository.DeleteAsync(
            companyId,
            guestId);
    }

    private static GuestDto MapToDto(Guest guest)
    {
        return new GuestDto
        {
            GuestId = guest.GuestId,
            CompanyId = guest.CompanyId,
            FirstName = guest.FirstName,
            LastName = guest.LastName,
            DocumentType = guest.DocumentType,
            DocumentNumber = guest.DocumentNumber,
            Email = guest.Email,
            Phone = guest.Phone,
            Nationality = guest.Nationality,
            Address = guest.Address,
            City = guest.City,
            CountryCode = guest.CountryCode,
            Notes = guest.Notes,
            IsActive = guest.IsActive
        };
    }
}