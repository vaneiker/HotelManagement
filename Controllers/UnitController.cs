using HotelManagement.Application.DTOs.Units;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/properties/{propertyId:int}/units")]
public class UnitController : ControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int propertyId)
    {
        var result = await _unitService
            .GetAllAsync(propertyId);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int propertyId,
        int id)
    {
        var result = await _unitService
            .GetByIdAsync(propertyId, id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int propertyId,
        CreateUnitRequest request)
    {
        var id = await _unitService
            .CreateAsync(propertyId, request);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                propertyId,
                id
            },
            new
            {
                unitId = id
            });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int propertyId,
        int id,
        UpdateUnitRequest request)
    {
        var updated = await _unitService
            .UpdateAsync(propertyId, id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int propertyId,
        int id)
    {
        var deleted = await _unitService
            .DeleteAsync(propertyId, id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}