namespace HotelManagement.Application.DTOs.Guests
{
    public class GuestDto
    {
        public long GuestId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? CountryCode { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
