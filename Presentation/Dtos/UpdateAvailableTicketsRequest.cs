namespace Presentation.Dtos;

public class UpdateAvailableTicketsRequest
{
    public string EventId { get; set; } = null!;
    public int NewAvailableTickets { get; set; }
}
