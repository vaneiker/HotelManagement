namespace HotelManagement.Application.DTOs.Roles;

public class CreateRoleRequest
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}