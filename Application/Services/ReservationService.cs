using HotelManagement.Application.DTOs.Reservations;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(
        IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync(
        int companyId)
    {
        var reservations =
            await _reservationRepository.GetAllAsync(companyId);

        return reservations.Select(MapToDto);
    }

    public async Task<ReservationDto?> GetByIdAsync(
        int companyId,
        long reservationId)
    {
        var reservation =
            await _reservationRepository.GetByIdAsync(
                companyId,
                reservationId);

        if (reservation == null)
            return null;

        return MapToDto(reservation);
    }

    public async Task<long> CreateAsync(
        int companyId,
        CreateReservationRequest request)
    {
        var reservation = new Reservation
        {
            CompanyId = companyId,
            PropertyId = request.PropertyId,
            UnitId = request.UnitId,
            GuestId = request.GuestId,
            ReservationNumber = request.ReservationNumber,
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            Adults = request.Adults,
            Children = request.Children,
            Status = request.Status,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        return await _reservationRepository.CreateAsync(
            reservation);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        long reservationId,
        UpdateReservationRequest request)
    {
        var reservation =
            await _reservationRepository.GetByIdAsync(
                companyId,
                reservationId);

        if (reservation == null)
            return false;

        reservation.PropertyId = request.PropertyId;
        reservation.UnitId = request.UnitId;
        reservation.GuestId = request.GuestId;
        reservation.ReservationNumber = request.ReservationNumber;
        reservation.CheckInDate = request.CheckInDate;
        reservation.CheckOutDate = request.CheckOutDate;
        reservation.Adults = request.Adults;
        reservation.Children = request.Children;
        reservation.Status = request.Status;
        reservation.Notes = request.Notes;
        reservation.UpdatedAt = DateTime.UtcNow;

        return await _reservationRepository.UpdateAsync(
            reservation);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long reservationId)
    {
        return await _reservationRepository.DeleteAsync(
            companyId,
            reservationId);
    }

    private static ReservationDto MapToDto(
        Reservation reservation)
    {
        return new ReservationDto
        {
            ReservationId = reservation.ReservationId,
            CompanyId = reservation.CompanyId,
            PropertyId = reservation.PropertyId,
            UnitId = reservation.UnitId,
            GuestId = reservation.GuestId,
            ReservationNumber = reservation.ReservationNumber,
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate,
            Adults = reservation.Adults,
            Children = reservation.Children,
            Status = reservation.Status,
            Notes = reservation.Notes
        };
    }
}