namespace Domein.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime EventDateTime { get; set; }
}