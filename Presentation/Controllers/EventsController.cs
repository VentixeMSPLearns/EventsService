using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
}