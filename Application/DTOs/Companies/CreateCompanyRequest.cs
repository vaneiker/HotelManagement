namespace HotelManagement.Application.DTOs.Companies;

public class CreateCompanyRequest
{
    public string Name { get; set; } = string.Empty;
    public string? LegalName { get; set; }
    public string? Rnc { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string CountryCode { get; set; } = "DO";
    public string CurrencyCode { get; set; } = "DOP";
}