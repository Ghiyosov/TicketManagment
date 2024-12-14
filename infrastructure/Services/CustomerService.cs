using System.Net;
using Dapper;
using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.DataContext;

namespace infrastructure.Services;

public class CustomerService : IServices<Customer>
{
    readonly DapperContext _context;

    public CustomerService()
    {
        _context = new DapperContext();
    }

    public Response<List<Customer>> GetAll()
    {
        var sql = @"select * from customers";
        var res = _context.GetConnection().Query<Customer>(sql).ToList();
        return new Response<List<Customer>>(res);
    }

    public Response<Customer> GetById(int id)
    {
        var sql = "select * from customers where CustomerId = @Id";
        var res = _context.GetConnection().QueryFirstOrDefault<Customer>(sql, new { Id = id });
        return new Response<Customer>(res);
    }

    public Response<string> Create(Customer entity)
    {
        var sql = "insert into Customers (CustomerId, FullName, Email, Phone) values (@CustomerId, @FullName, @Email, @Phone) returning 'Customer : '||FullName;";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, entity);
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError,  "Internal Server Error")
            : new Response<string>(HttpStatusCode.Created,$"{res} created successfully");
    }

    public Response<string> Update(Customer entity)
    {
        var sql = "update customers set FullName = @FullName, Email = @Email, Phone = @Phone where CustomerId = @CustomerId returning 'Customer : '||CustomerId;";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, entity);
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError,  "Internal Server Error")
            : new Response<string>(HttpStatusCode.OK,$"{res} update successfully");
    }

    public Response<string> Delete(int id)
    {
        var sql = "delete from customers where CustomerId = @Id returning 'Customer : '||Fullname||' Id : '||CustomerId;";
        var res = _context.GetConnection().ExecuteScalar<string>(sql, new { Id = id });
        return res == null
            ? new Response<string>(HttpStatusCode.InternalServerError,  "Internal Server Error")
            : new Response<string>(HttpStatusCode.OK,$"{res} delete successfully");
    }
}