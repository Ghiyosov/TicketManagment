using Domein.Models;
using infrastructure.ApiResponse;
using infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("[controller]")]
public class LocationController: ControllerBase
{
    readonly LocationService _locationService;

    public LocationController()
    {
        _locationService = new LocationService();
    }

    [HttpGet("GetAllLocations")]
    public Response<List<Location>> GetAllLocations()
    {
        return _locationService.GetAll();
    }

    [HttpGet("GetLocationById/{id}")]
    public Response<Location> GetLocationById(int id)
    {
        return _locationService.GetById(id);
    }

    [HttpPost("AddLocation")]
    public Response<string> AddLocation(Location location)
    {
        return _locationService.Create(location);
    }

    [HttpPut("UpdateLocation")]
    public Response<string> UpdateLocation(Location location)
    {
        return _locationService.Update(location);
    }

    [HttpDelete("DeleteLocation/{id}")]
    public Response<string> DeleteLocation(int id)
    {
        return _locationService.Delete(id);
    }
}