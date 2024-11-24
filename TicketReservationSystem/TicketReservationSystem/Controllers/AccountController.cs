using Microsoft.AspNetCore.Mvc;
using TicketReservationSystem.DTO;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPut("complete-data")]
        public ActionResult CompleteData([FromBody] CustomerDataDto dto)
        {
            var user = User;
            _accountService.CompleteData(dto, user);
            return Ok();
        }
    }
}
