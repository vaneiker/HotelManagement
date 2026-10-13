using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync(int companyId);

    Task<Reservation?> GetByIdAsync(
        int companyId,
        long reservationId);

    Task<long> CreateAsync(Reservation reservation);

    Task<bool> UpdateAsync(Reservation reservation);

    Task<bool> DeleteAsync(
        int companyId,
        long reservationId);
}