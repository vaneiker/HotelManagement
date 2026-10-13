namespace HotelManagement.Application.DTOs.PropertyImages;

public class UpdatePropertyImageRequest
{
    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }

    public bool IsActive { get; set; }
}