namespace HotelManagement.Domain.Entities
{
    public class Unit
    {
        public int UnitId { get; set; }
        public int PropertyId { get; set; }
        public int UnitTypeId { get; set; }

        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; }

        public int? FloorNumber { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
