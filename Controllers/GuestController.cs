using HotelManagement.Application.DTOs.Guests;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var result = await _guestService.GetAllAsync(companyId);

        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var result = await _guestService.GetByIdAsync(
            companyId,
            id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateGuestRequest request)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var id = await _guestService.CreateAsync(
            companyId,
            request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { guestId = id });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        UpdateGuestRequest request)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var updated = await _guestService.UpdateAsync(
            companyId,
            id,
            request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var deleted = await _guestService.DeleteAsync(
            companyId,
            id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}