using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync(
        int companyId)
    {
        return await _repository.GetAllAsync(companyId);
    }

    public async Task<PaymentDto?> GetByIdAsync(
        int companyId,
        long paymentId)
    {
        return await _repository.GetByIdAsync(
            companyId,
            paymentId);
    }

    public async Task<IEnumerable<PaymentDto>> GetByReservationAsync(
        int companyId,
        long reservationId)
    {
        return await _repository.GetByReservationAsync(
            companyId,
            reservationId);
    }

    public async Task<long> CreateAsync(
        CreatePaymentRequest request)
    {
        return await _repository.CreateAsync(request);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        long paymentId,
        UpdatePaymentRequest request)
    {
        return await _repository.UpdateAsync(
            companyId,
            paymentId,
            request);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long paymentId)
    {
        return await _repository.DeleteAsync(
            companyId,
            paymentId);
    }
}