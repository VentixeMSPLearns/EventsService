using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        
        var result = await _eventService.GetAllEventsAsync();
        if (result.Success && result.Data != default)
        {
            var events = result.Data;
            return Ok(events);
        }
        return Problem(detail: result.ErrorMessage);

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _eventService.GetEventByIdAsync(id);
        if (result.Success && result.Data != default)
        {
            var model = result.Data;
            return Ok(model);
        }
        return Problem(detail: result.ErrorMessage);
    }

    [HttpPatch("update-tickets")]
    public async Task<IActionResult> UpdateAvailableTickets(UpdateAvailableTicketsRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.EventId) || request.NewAvailableTickets < 0)
        {
            return BadRequest("Invalid request data.");
        }

        try
        {
            var result = await _eventService.UpdateAvailableTicketsAsync(
                request.EventId,
                request.NewAvailableTickets);

            if (result.Success)
            {
                return Ok("Available tickets updated successfully.");
            }

            return Problem($"Error: {result.ErrorMessage}");
        }
        catch (Exception ex)
        {
            return Problem($"Error: {ex.Message}");
        }
    }

    [HttpPost("by-ids")]
    public async Task<IActionResult> GetEventsByIds(EventsListRequest request)
    {
        if (request == null || request.EventIds == null || !request.EventIds.Any())
        {
            return BadRequest("EventIds list is required.");
        }

        var result = await _eventService.GetEventsByIdListAsync(request.EventIds);

        if (result.Success && result.Data != default)
        {
            var events = result.Data;
            return Ok(events);
        }

        return Problem(detail: result.ErrorMessage);
    }

}