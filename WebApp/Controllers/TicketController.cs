using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    readonly TicketService _ticket;

    public TicketController()
    {
        _ticket = new TicketService();
    }

    [HttpGet("GetAllTickets")]
    public Response<List<Ticket>> GetAllTickets()
    {
        return _ticket.GetAll();
    }

    [HttpGet("GetTicketById/{id}")]
    public Response<Ticket> GetTicketById(int id)
    {
        return _ticket.GetById(id);
    }

    [HttpPost("AddTicket")]
    public Response<string> AddTicket(Ticket ticket)
    {
        return _ticket.Create(ticket);
    }

    [HttpPut("UpdateTicket")]
    public Response<string> UpdateTicket(Ticket ticket)
    {
        return _ticket.Update(ticket);
    }

    [HttpDelete("DeleteTicket/{id}")]
    public Response<string> DeleteTicket(int id)
    {
        return _ticket.Delete(id);
    }
    
    
    
}