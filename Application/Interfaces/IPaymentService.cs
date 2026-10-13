using HotelManagement.Application.DTOs.Payments;

namespace HotelManagement.Application.Interfaces;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllAsync(
        int companyId);

    Task<PaymentDto?> GetByIdAsync(
        int companyId,
        long paymentId);

    Task<IEnumerable<PaymentDto>> GetByReservationAsync(
        int companyId,
        long reservationId);

    Task<long> CreateAsync(
        CreatePaymentRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        long paymentId,
        UpdatePaymentRequest request);

    Task<bool> DeleteAsync(
        int companyId,
        long paymentId);
}