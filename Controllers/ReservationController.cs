using HotelManagement.Application.DTOs.Reservations;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(
        IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var result =
            await _reservationService.GetAllAsync(companyId);

        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var result =
            await _reservationService.GetByIdAsync(
                companyId,
                id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateReservationRequest request)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var id =
            await _reservationService.CreateAsync(
                companyId,
                request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { reservationId = id });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        UpdateReservationRequest request)
    {
        var companyId = 1; // TEMPORAL hasta JWT

        var updated =
            await _reservationService.UpdateAsync(
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

        var deleted =
            await _reservationService.DeleteAsync(
                companyId,
                id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}