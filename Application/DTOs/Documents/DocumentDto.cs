
namespace HotelManagement.Application.DTOs.Documents;

public class DocumentDto
{
    public long DocumentId { get; set; }
    public int CompanyId { get; set; }

    public string? EntityType { get; set; }
    public long? EntityId { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string? OriginalFileName { get; set; }

    public string BucketName { get; set; } = string.Empty;
    public string ObjectKey { get; set; } = string.Empty;

    public string? ContentType { get; set; }
    public long? FileSize { get; set; }

    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    // URL temporal generada por MinIO.
    public string? Url { get; set; }
}