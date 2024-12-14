using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketLocationController: ControllerBase
{
    readonly TicketLocationService _ticketLocationService;

    public TicketLocationController()
    {
        _ticketLocationService = new TicketLocationService();
    }

    [HttpPost("CreateTicketLocation")]
    public Response<string> CreateTicketLocation(TiketLocation ticketLocation)
    {
        return _ticketLocationService.CreateTicketLocation(ticketLocation);
    }

    [HttpDelete("DeleteTicketLocation")]
    public Response<string> DeleteTicketLocation(TiketLocation ticketLocation)
    {
        return _ticketLocationService.DeleteTicketLocation(ticketLocation);
    }

    [HttpGet("GetAllTicketsByLocations")]
    public Response<List<Ticket>> GetAllTicketsByLocations(int locationId)
    {
        return _ticketLocationService.GetTicketsByLocations(locationId);
    }
}
