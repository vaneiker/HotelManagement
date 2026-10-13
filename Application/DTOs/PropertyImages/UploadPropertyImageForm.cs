using Microsoft.AspNetCore.Http;

namespace HotelManagement.Application.DTOs.PropertyImages;

public class UploadPropertyImageForm
{
    public IFormFile File { get; set; } = default!;

    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }
}