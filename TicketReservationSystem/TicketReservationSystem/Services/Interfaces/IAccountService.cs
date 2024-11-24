using System.Security.Claims;
using TicketReservationSystem.DTO;

namespace TicketReservationSystem.Services.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);
        void RegisterUser(RegisterUserDto dto);
        void CompleteData(CustomerDataDto dto, ClaimsPrincipal user);
    }
}
