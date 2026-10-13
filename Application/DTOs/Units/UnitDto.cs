namespace HotelManagement.Application.DTOs.Units
{
    public class UnitDto
    {
        public int UnitId { get; set; }
        public int PropertyId { get; set; }
        public int UnitTypeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; }
        public int? FloorNumber { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
