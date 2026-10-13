using HotelManagement.Application.DTOs.Users;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int companyId)
    {
        var result =
            await _userService.GetAllAsync(companyId);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        [FromQuery] int companyId)
    {
        var result =
            await _userService.GetByIdAsync(
                companyId,
                id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserRequest request)
    {
        var id =
            await _userService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                id,
                companyId = request.CompanyId
            },
            new
            {
                userId = id
            });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromQuery] int companyId,
        UpdateUserRequest request)
    {
        var updated =
            await _userService.UpdateAsync(
                companyId,
                id,
                request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromQuery] int companyId)
    {
        var deleted =
            await _userService.DeleteAsync(
                companyId,
                id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}