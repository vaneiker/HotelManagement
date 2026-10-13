namespace HotelManagement.Domain.Entities
{
    public class PropertyImage
    {
        public long PropertyImageId { get; set; }

        public int CompanyId { get; set; }
        public int PropertyId { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string? OriginalFileName { get; set; }

        public string BucketName { get; set; } = string.Empty;
        public string ObjectKey { get; set; } = string.Empty;

        public string? ContentType { get; set; }
        public long? FileSize { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
