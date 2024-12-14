using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("[controller]")]
public class CustomerController: ControllerBase
{
    readonly CustomerService _customerService;

    public CustomerController()
    {
        _customerService = new CustomerService();
    }

    [HttpGet("GetAllCustomers")]
    public Response<List<Customer>> GetAllCustomers()
    {
        return _customerService.GetAll();
    }

    [HttpGet("GetCustomerById/{id}")]
    public Response<Customer> GetCustomerById(int id)
    {
        return _customerService.GetById(id);
    }

    [HttpPost("AddCustomer")]
    public Response<string> AddCustomer([FromBody] Customer customer)
    {
        return _customerService.Create(customer);
    }

    [HttpPut("UpdateCustomer")]
    public Response<string> UpdateCustomer([FromBody] Customer customer)
    {
        return _customerService.Update(customer);
    }

    [HttpDelete("DeleteCustomer/{id}")]
    public Response<string> DeleteCustomer(int id)
    {
        return _customerService.Delete(id);
    }
}
