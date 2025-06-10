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
}

public interface IEventRepository
{
    Task<Result<IEnumerable<EventEntity>>> GetAllEventsAsync();
    Task<Result<EventEntity>> GetEventByIdAsync(string id);
}