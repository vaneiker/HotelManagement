namespace HotelManagement.Application.DTOs.UnitTypes
{
    public class UpdateUnitTypeRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int BaseCapacity { get; set; }
        public bool IsActive { get; set; }
    }
}
