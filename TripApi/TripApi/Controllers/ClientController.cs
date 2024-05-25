namespace TripApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using TripApi.Services;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{idClient}")]
    public IActionResult RemoveClient([FromRoute] int idClient)
    {
        try
        {
            _clientService.RemoveClient(idClient);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}