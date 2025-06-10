namespace Business.Models;

public class EventModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int TotalTickets { get; set; }
    public int AvailableTickets { get; set; }
    public bool IsSoldOut { get; set; } = false;
}
