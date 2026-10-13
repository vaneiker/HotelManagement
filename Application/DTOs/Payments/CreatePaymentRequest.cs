namespace HotelManagement.Application.DTOs.Payments;

public class CreatePaymentRequest
{
    public int CompanyId { get; set; }
    public long ReservationId { get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; } = "DOP";
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public DateTime PaymentDate { get; set; }
}