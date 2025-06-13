using Data.Context;
using Data.Entities;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EventRepository : IEventRepository
{
    protected readonly DataContext _context;
    protected readonly DbSet<EventEntity> _events;
    public EventRepository(DataContext context)
    {
        _context = context;
        _events = _context.Set<EventEntity>();
    }
    public async Task<Result<IEnumerable<EventEntity>>> GetAllEventsAsync()
    {
        try
        {
            var entities = await _events.ToListAsync();
            if (entities == null || entities.Count == 0)
            {
                return new Result<IEnumerable<EventEntity>>
                {
                    Success = false,
                    ErrorMessage = "No events found."
                };
            }
            return new Result<IEnumerable<EventEntity>>
            {
                Success = true,
                Data = entities
            };

        }
        catch (Exception ex)
        {

            return new Result<IEnumerable<EventEntity>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<Result<EventEntity>> GetEventByIdAsync(string id)
    {
        try
        {
            var entity = await _events.FindAsync(id);
            if (entity == null)
            {
                return new Result<EventEntity>
                {
                    Success = false,
                    ErrorMessage = "Event not found."
                };
            }
            return new Result<EventEntity>
            {
                Success = true,
                Data = entity
            };

        }
        catch (Exception ex)
        {
            return new Result<EventEntity>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<Result<EventEntity>> UpdateAvailableTicketsAsync(string eventId, int newAvailableTickets)
    {
        try
        {
            var rowsAffected = await _events
                .Where(e => e.Id == eventId)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(e => e.AvailableTickets, newAvailableTickets));

            if (rowsAffected > 0)
            {
                return new Result<EventEntity> { Success = true };
            }
            else
            {
                return new Result<EventEntity>
                {
                    Success = false,
                    ErrorMessage = "No changes were saved to the database."
                };
            }
        }
        catch (Exception ex)
        {
            return new Result<EventEntity>
            {
                Success = false,
                ErrorMessage = $"An error occurred while updating available tickets: {ex.Message}"
            };
        }
    }

    public async Task<Result<IEnumerable<EventEntity>>> GetEventsByIdListAsync(List<string> eventIds)
    {
        try
        {
            var entities = await _events
                .Where(e => eventIds.Contains(e.Id)) //Checks if the event e Id is in the provided list of eventIds
                .ToListAsync();

            if (entities == null || entities.Count == 0)
            {
                return new Result<IEnumerable<EventEntity>>
                {
                    Success = false,
                    ErrorMessage = "No events found for the provided IDs."
                };
            }

            return new Result<IEnumerable<EventEntity>>
            {
                Success = true,
                Data = entities
            };
        }
        catch (Exception ex)
        {
            return new Result<IEnumerable<EventEntity>>
            {
                Success = false,
                ErrorMessage = $"An error occurred while fetching events: {ex.Message}"
            };
        }
    }
}

