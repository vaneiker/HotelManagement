namespace HotelManagement.Application.DTOs.Finance;

public sealed class MonthlySummaryDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetCashFlow { get; set; }
}