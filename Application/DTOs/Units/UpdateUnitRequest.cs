namespace HotelManagement.Application.DTOs.Units
{
    public class UpdateUnitRequest
    {
        public int UnitTypeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; }
        public int? FloorNumber { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
