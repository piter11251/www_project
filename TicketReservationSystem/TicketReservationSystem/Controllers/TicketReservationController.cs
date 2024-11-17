using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Entities;

namespace TicketReservationSystem.Controllers
{
    [Route("api/reservation")]
    public class TicketReservationController: ControllerBase
    {
        private readonly TicketSystemDbContext _context;
        public TicketReservationController(TicketSystemDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event request)
        {
            try
            {
                if(request.Rows <= 0 || request.TotalSeats <= 0 || request.TotalSeats % request.Rows != 0)
                {
                    return BadRequest("Łączna liczba miejsc musi być podzielna przez liczbę rzędów");
                }

                var newEvent = new Event
                {
                    Name = request.Name,
                    Description = request.Description,
                    TicketPrice = request.TicketPrice,
                    TotalSeats = request.TotalSeats,
                    Rows = request.Rows
                };

                int seatsPerRow = request.TotalSeats / request.Rows;
                for(int row = 1; row <= request.Rows; row++)
                {
                    for(int number=1; number<=seatsPerRow; number++)
                    {
                        newEvent.Seats.Add(new Seat
                        {
                            Row = row,
                            SeatNumber = number,
                            IsReserved = true
                        });
                    }
                }
                _context.Events.Add(newEvent);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Wystapil blad: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetEvent(int id)
        {
            var ev = _context.Events
                .Include(e => e.Seats)
                .FirstOrDefault(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            return Ok(ev);
        }
    }
}
