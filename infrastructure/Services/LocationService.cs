using System.Net;
using Dapper;
using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.DataContext;

namespace infrastructure.Services;

public class LocationService : IServices<Location>
{
    readonly DapperContext _context;

    public LocationService()
    {
        _context = new DapperContext();
    }


    public Response<List<Location>> GetAll()
    {
        var sql = @"select * from Locations";
        var res = _context.GetConnection().Query<Location>(sql).ToList();
        return new Response<List<Location>>(res);
    }

    public Response<Location> GetById(int id)
    {
        var sql = @"select * from Locations where LocationId = @Id";
        var res = _context.GetConnection().QueryFirstOrDefault<Location>(sql, new { Id = id });
        return new Response<Location>(res);
    }

    public Response<string> Create(Location entity)
    {
        var sql = @"insert into Locations (City, Address, LocationType) values (@City, @Address, @LocationType) returning LocationId||' '||City||' '||Address";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, entity);
        return res ==null 
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<string>(HttpStatusCode.Created, $"Location {res} has been created");
    }

    public Response<string> Update(Location entity)
    {
        var sql = "update Locations set City=@City, Address=@Address, LocationType=@LocationType where LocationId = @LocationId returning LocationId";
        var res = _context.GetConnection().ExecuteScalar<int>(sql, entity);
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<string>(HttpStatusCode.Created, $"Location {res} has been updated");
    }

    public Response<string> Delete(int id)
    {
        var sql = @"delete from Locations where LocationId = @Id returning LocationId||' '||City||' '||Addres";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, new { Id = id });
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<string>(HttpStatusCode.Created, $"Location {res} has been deleted");
    }
}