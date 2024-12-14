using System.Net;
using Dapper;
using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.DataContext;

namespace infrastructure.Services;

public class TicketService : IServices<Ticket>
{
    readonly DapperContext _context;

    public TicketService()
    {
        _context = new DapperContext();
    }
    
    public Response<List<Ticket>> GetAll()
    {
        var sql = "SELECT * FROM Tickets";
        var res = _context.GetConnection().Query<Ticket>(sql).ToList();
        return new Response<List<Ticket>>(res);
    }

    public Response<Ticket> GetById(int id)
    {
        var sql = "SELECT * FROM Tickets WHERE TicketId = @Id";
        var res = _context.GetConnection().QueryFirstOrDefault<Ticket>(sql, new { Id = id });
        return new Response<Ticket>(res);
    }

    public Response<string> Create(Ticket entity)
    {
        var sql = "insert into Tickets(Type,Title,Price,EventDateTime)values(@Type,@Title,@Price,@EventDateTime)returning 'Ticket : '||Type||' - '||Title";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, entity);
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Unable to create ticket")
            : new Response<string>(HttpStatusCode.Created, $"{res} is created successfully");
    }

    public Response<string> Update(Ticket entity)
    {
        var sql = "update Tickets set Type=@Type,Title=@Title,Price=@Price,EventDateTime=@EventDateTime where TicketId = @TicketId returning 'Ticket ID: '||TicketId";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, entity);
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Unable to update ticket")
            : new Response<string>(HttpStatusCode.OK, $"Updated {res} is successfully");
    }

    public Response<string> Delete(int id)
    {
        var sql = "delete from Tickets where TicketId = @TicketId returning 'Ticket ID: '||TicketId";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, new { Id = id });
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Unable to delete ticket")
            : new Response<string>(HttpStatusCode.OK, $"Deleted {res} is successfully");
    }
}