using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.DTO;
using TicketReservationSystem.Entities;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class TicketReservationController: ControllerBase
    {
        private readonly TicketSystemDbContext _context;
        private readonly IEventService _eventService;
        public TicketReservationController(TicketSystemDbContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }
        [HttpPost]
        public ActionResult CreateEvent([FromBody] EventCreateDto request)
        {
            _eventService.CreateEventWithGeneratedSeats(request);
            return Ok();
        }

        [HttpGet("{id}/eventdetails")]
        public ActionResult<EventDetailsDTO> GetEventDetails([FromRoute] int id)
        {
            var eventDetails = _eventService.GetEventDetails(id);
            return Ok(eventDetails);
        }

        [HttpGet("{id}/seats")]
        public ActionResult<ICollection<SeatDTO>> GetSeatsDetails([FromRoute]int id)
        {
            var seatsDetails = _eventService.GetSeats(id);
            return Ok(seatsDetails);
        }

    }
}
