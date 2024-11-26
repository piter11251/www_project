using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketReservationSystem.DTO.PaymentDto;
using TicketReservationSystem.Services;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize]
    public class PaymentController: ControllerBase
    {    
        private readonly TicketSystemDbContext _context;
        private readonly IPaymentService _paymentService;
        public PaymentController(TicketSystemDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }
        [HttpPost("initiate-payment")]
        public ActionResult InitiatePayment([FromBody] PaymentDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid User");
            }
            _paymentService.InitiatePayment(dto);
            return Ok();
        }

        [HttpPatch("complete-payment")]
        public ActionResult CompletePayment([FromBody] PaymentDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var customerId))
            {
                return Unauthorized("Invalid user.");
            }

            _paymentService.CompletePayment(dto);

            return Ok("Payment Successful.");
        }
    }
}
