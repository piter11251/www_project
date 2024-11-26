using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketReservationSystem.DTO.ReservationDto;
using TicketReservationSystem.Exceptions;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly TicketSystemDbContext _context;
        private readonly IReservationService _reservationService;
        public ReservationController(TicketSystemDbContext context, IReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }
        [HttpPost]
        public ActionResult CreateReservation([FromBody] ReservationCreateDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user");
            }
            _reservationService.CreateReservation(dto, User);
            return Ok();
        }
        [HttpGet("get-reservations")]
        public ActionResult GetReservations()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user");
            }
            var reservations = _reservationService.GetReservationDetails(userId); ;
            return Ok(reservations);
        }

        [HttpDelete]
        public ActionResult CancelReservation([FromRoute] string reservationNumber)
        {
            _reservationService.CancelReservation(reservationNumber);
            return Ok(new { message = "Reservation cancelled" });
        }

        [HttpPatch]
        public ActionResult ModifyReservation([FromBody] ReservationModifyDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user.");
            }

            try
            {
                _reservationService.UpdateSeats(dto, userId);
                return Ok("Seats successfully updated.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
