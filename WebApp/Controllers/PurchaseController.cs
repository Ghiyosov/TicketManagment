using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController: ControllerBase
{
    readonly PurshaseService _purshaseService;

    public PurchaseController()
    {
        _purshaseService = new PurshaseService();
    }

    [HttpGet("GetAllPurshases")]
    public Response<List<Purchase>> GetAllPurshases()
    {
        return _purshaseService.GetPurchases();
    }

    [HttpGet("GetPurchaseByCustomerId/Customer{id}")]
    public Response<List<Purchase>> GetPurchaseByCustomerId(int id)
    {
        return _purshaseService.GetPurchasesByCustomerId(id);
    }

    [HttpPost("AddPurchase")]
    public Response<string> AddPurchase(Purchase purchase)
    {
        return _purshaseService.Creat(purchase);
    }
}