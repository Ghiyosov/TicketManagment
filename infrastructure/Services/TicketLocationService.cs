using System.Net;
using Dapper;
using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.DataContext;

namespace infrastructure.Services;

public class TicketLocationService
{
    readonly DapperContext _context;

    public TicketLocationService()
    {
        _context = new DapperContext();
    }

    public Response<string> CreateTicketLocation(TiketLocation ticketLocation)
    {
        var sqlTicket =@"select * from Tickets where TicketId=@TicketId";
        var resTic = _context.GetConnection().QueryFirstOrDefault<Ticket>(sqlTicket,new{TicketId = ticketLocation.TicketId});
        var sqlLocation =@"select * from Locations where LocationId=@LocationId";
        var resLoc = _context.GetConnection().QueryFirstOrDefault<Location>(sqlLocation,new { LocationId = ticketLocation.LocationId });
        if (resTic == null && resLoc == null)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, "No ticket, No location found");
        }

        var sql = @"insert into TicketLocations (TicketId, LocationId) values (@TicketId, @LocationId)";
        var res = _context.GetConnection().Execute(sqlLocation,ticketLocation);
        return new Response<string>(HttpStatusCode.Created, $"Ticket : {resTic.Type}-{resTic.Title} binding to location {resLoc.City}-{resLoc.Address} succesfully");
    }

    public Response<string> DeleteTicketLocation(TiketLocation ticketLocation)
    {
        var sqlTicket =@"select * from Tickets where TicketId=@TicketId";
        var resTic = _context.GetConnection().QueryFirstOrDefault<Ticket>(sqlTicket,new{TicketId = ticketLocation.TicketId});
        var sqlLocation =@"select * from Locations where LocationId=@LocationId";
        var resLoc = _context.GetConnection().QueryFirstOrDefault<Location>(sqlLocation,new { LocationId = ticketLocation.LocationId });
        if (resTic == null && resLoc == null)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, "No ticket, No location found");
        }
        var sql = @"delete from TicketLocations where TicketId=@TicketId and LocationId=@LocationId";
        var res = _context.GetConnection().Execute(sql,ticketLocation);
        return new Response<string>(HttpStatusCode.Created, $"Binding Ticket : {resTic.Type}-{resTic.Title}  to location {resLoc.City}-{resLoc.Address} deleted succesfully");

    }

    public Response<List<Ticket>> GetTicketsByLocations(int locationId)
    {
        var sqlTicket =@"select * from Tickets as t
                            join TicketLocations as tl on t.TicketId=tl.TicketId
                                where tl.LocationId=@LocationId;";
        var res = _context.GetConnection().Query<Ticket>(sqlTicket,new{LocationId = locationId}).ToList();
        return new Response<List<Ticket>>(res);
    }
}

