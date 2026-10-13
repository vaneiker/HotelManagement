using HotelManagement.Application.DTOs.Reservations;

namespace HotelManagement.Application.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllAsync(
        int companyId);

    Task<ReservationDto?> GetByIdAsync(
        int companyId,
        long reservationId);

    Task<long> CreateAsync(
        int companyId,
        CreateReservationRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        long reservationId,
        UpdateReservationRequest request);

    Task<bool> DeleteAsync(
        int companyId,
        long reservationId);
}