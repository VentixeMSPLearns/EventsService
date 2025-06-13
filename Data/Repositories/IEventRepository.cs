using Data.Entities;
using Data.Models;

namespace Data.Repositories
{
    public interface IEventRepository
    {
        Task<Result<IEnumerable<EventEntity>>> GetAllEventsAsync();
        Task<Result<EventEntity>> GetEventByIdAsync(string id);
        Task<Result<IEnumerable<EventEntity>>> GetEventsByIdListAsync(List<string> eventIds);
        Task<Result<EventEntity>> UpdateAvailableTicketsAsync(string eventId, int newAvailableTickets);
    }
}