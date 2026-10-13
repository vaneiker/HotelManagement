
using HotelManagement.Application.DTOs.Finance;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FinanceController : ControllerBase
{
    private readonly IMonthlySummaryService _monthlySummaryService;

    public FinanceController(
        IMonthlySummaryService monthlySummaryService)
    {
        _monthlySummaryService = monthlySummaryService;
    }

    [HttpGet("properties/{propertyId:int}/monthly-summary")]
    public async Task<ActionResult<IEnumerable<MonthlySummaryDto>>>
        GetMonthlySummary(
            [FromRoute] int propertyId,
            [FromQuery] int companyId,
            [FromQuery] int year)
    {
        if (companyId <= 0 || propertyId <= 0)
            return BadRequest("La compañía y la propiedad deben ser válidas.");

        if (year < 2000 || year > 2100)
            return BadRequest("El año debe estar entre 2000 y 2100.");

        var summary =
            await _monthlySummaryService.GetMonthlySummaryAsync(
                companyId,
                propertyId,
                year);

        return Ok(summary);
    }
}