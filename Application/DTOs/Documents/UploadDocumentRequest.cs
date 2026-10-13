namespace HotelManagement.Application.DTOs.Documents
{
    public class UploadDocumentRequest
    {
        public IFormFile File { get; set; } = default!;

        public int CompanyId { get; set; }

        public string? EntityType { get; set; }

        public long? EntityId { get; set; }
    }
}
