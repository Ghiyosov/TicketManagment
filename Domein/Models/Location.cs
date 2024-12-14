namespace Domein.Models;

public class Location
{
    public int LocationId { get; set; }
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LocationType { get; set; } = string.Empty;
}