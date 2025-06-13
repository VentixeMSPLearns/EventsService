using Business.Models;
using Data.Models;

namespace Business.Services
{
    public interface IEventService
    {
        Task<Result<IEnumerable<EventModel>>> GetAllEventsAsync();
        Task<Result<EventModel>> GetEventByIdAsync(string id);
        Task<Result<IEnumerable<EventModel>>> GetEventsByIdListAsync(List<string> eventIds);
        Task<Result<EventModel?>> UpdateAvailableTicketsAsync(string eventId, int newAvailableTickets);
    }
}