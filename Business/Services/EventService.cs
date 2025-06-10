
using Business.Models;
using Data.Entities;
using Data.Models;
using Data.Repositories;

namespace Business.Services;

public interface IEventService
{
    Task<Result<IEnumerable<EventModel>>> GetAllEventsAsync();
    Task<Result<EventModel>> GetEventByIdAsync(string id);
}

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;
    public async Task<Result<IEnumerable<EventModel>>> GetAllEventsAsync()
    {
        var result = await _eventRepository.GetAllEventsAsync();
        if (result.Success && result.Data != default)
        {
            var events = result.Data.Select(entity => MapEntityToModel(entity)!).ToList();

            return new Result<IEnumerable<EventModel>>
            {
                Success = true,
                Data = events
            };
        }
        return new Result<IEnumerable<EventModel>>
        {
            Success = false,
            ErrorMessage = result.ErrorMessage
        };
    }

    public async Task<Result<EventModel>> GetEventByIdAsync(string id)
    {
        var result = await _eventRepository.GetEventByIdAsync(id);
        if (result.Success && result.Data != default)
        {
            var data = MapEntityToModel(result.Data);
            return new Result<EventModel>
            {
                Success = true,
                Data = data
            };
        }
        return new Result<EventModel>
        {
            Success = false,
            ErrorMessage = result.ErrorMessage
        };
    }

    public static EventModel? MapEntityToModel(EventEntity entity)
    {
        if (entity == null) return null;
        EventModel eventModel = new EventModel
        {
            Id = entity.Id,
            Title = entity.Title,
            DateTime = entity.DateTime,
            Location = entity.Location,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            TotalTickets = entity.TotalTickets,
            AvailableTickets = entity.AvailableTickets,
            Price = entity.Price
        };
        if (entity.AvailableTickets <= 0)
        {
            eventModel.IsSoldOut = true;
        }
        else
        {
            eventModel.IsSoldOut = false;
        }
        return eventModel;
    }
}