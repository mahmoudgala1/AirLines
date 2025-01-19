using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
  private readonly IMessageProducer _messageProducer;
  public static readonly List<Booking> _bookings = new List<Booking>();

  public BookingsController(IMessageProducer messageProducer)
  {
    _messageProducer = messageProducer;
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] Booking booking)
  {
    if (!ModelState.IsValid) return BadRequest();
    _bookings.Add(booking);
    await _messageProducer.SendingMessage(booking);
    return Ok();
  }
}