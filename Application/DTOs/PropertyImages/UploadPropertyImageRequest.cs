namespace HotelManagement.Application.DTOs.PropertyImages;

public class UploadPropertyImageRequest
{

    public Stream Stream { get; set; } = Stream.Null;

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }
}