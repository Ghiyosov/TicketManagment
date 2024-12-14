using System.Net;
using Dapper;
using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.DataContext;

namespace infrastructure.Services;

public class PurshaseService
{
    readonly DapperContext _context;

    public PurshaseService()
    {
        _context = new DapperContext();
    }

    public Response<string> Creat(Purchase purchase)
    {
        var sqlTicket = "select * from Tickets where TicketId=@Id";
        var resTicket = _context.GetConnection().QueryFirstOrDefault<Ticket>(sqlTicket,new {Id=purchase.TicketId});
        purchase.TotalPrice = resTicket.Price * purchase.Quantity;
        var sql = "insert into Purchases(TicketId, CustomerId, PurchaseDateTime, Quantity, TotalPrice) values (@TicketId, @CustomerId, @PurchaseDateTime, @Quantity, @TotalPrice) returning TotalPrice";
        var res = _context.GetConnection().ExecuteScalar<decimal>(sql,purchase);
        return res==null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Error creating ticket")
            : new Response<string>(HttpStatusCode.Created, $"Ticket has been created. Total price : {res}");
    }

    public Response<List<Purchase>> GetPurchases()
    {
        var sql = "select * from Purchases";
        var res = _context.GetConnection().Query<Purchase>(sql).ToList();
        return new Response<List<Purchase>>(res);
    }

    public Response<List<Purchase>> GetPurchasesByCustomerId(int customerId)
    {
        var sql = "select * from Purchases where CustomerId=@CustomerId";
        var res = _context.GetConnection().Query<Purchase>(sql, new {CustomerId = customerId}).ToList();
        return new Response<List<Purchase>>(res);
    }
    
}