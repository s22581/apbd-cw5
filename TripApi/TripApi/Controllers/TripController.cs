namespace TripApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using TripApi.Services;
using TripApi.Models.DTOs;

[ApiController]
[Route("api/trips")]
public class TripController : ControllerBase
{
    private readonly ITripService _service;

    public TripController(ITripService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetTrips()
    {
        try
        {
            var trips = _service.GetSortedTrips();
            return Ok(trips);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
    [HttpPost("{idTrip}/clients")]
    public IActionResult AssignClientToTrip([FromRoute] int idTrip, [FromBody] AssignClientToTripDto dto)
    {
        try
        {
            _service.AssignClientToTrip(idTrip, dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}