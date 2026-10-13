namespace HotelManagement.Application.DTOs.Properties
{
    public class UpdatePropertyRequest
    {
        public string Name { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string CountryCode { get; set; } = "DO";
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}
