using HotelManagement.Application.DTOs.Roles;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int companyId)
    {
        var result = await _service.GetAllAsync(companyId);

        return Ok(result);
    }

    [HttpGet("{roleId:int}")]
    public async Task<IActionResult> GetById(
        int roleId,
        [FromQuery] int companyId)
    {
        var result = await _service.GetByIdAsync(
            companyId,
            roleId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateRoleRequest request)
    {
        var roleId = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                roleId
            },
            new
            {
                RoleId = roleId
            });
    }

    [HttpPut("{roleId:int}")]
    public async Task<IActionResult> Update(
        int roleId,
        [FromQuery] int companyId,
        UpdateRoleRequest request)
    {
        var updated = await _service.UpdateAsync(
            companyId,
            roleId,
            request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> Delete(
        int roleId,
        [FromQuery] int companyId)
    {
        var deleted = await _service.DeleteAsync(
            companyId,
            roleId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}