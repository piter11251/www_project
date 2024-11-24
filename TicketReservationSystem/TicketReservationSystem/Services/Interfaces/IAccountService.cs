using TicketReservationSystem.DTO;

namespace TicketReservationSystem.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }
}
