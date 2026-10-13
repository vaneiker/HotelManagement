namespace HotelManagement.Domain.Entities
{
    public class UnitType
    {
        public int UnitTypeId { get; set; }
        public int PropertyId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int BaseCapacity { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
