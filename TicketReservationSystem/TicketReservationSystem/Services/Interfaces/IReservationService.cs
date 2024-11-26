using System.Security.Claims;
using TicketReservationSystem.DTO.ReservationDto;
using TicketReservationSystem.DTO.SeatDto;

namespace TicketReservationSystem.Services.Interfaces
{
    public interface IReservationService
    {
        void CreateReservation(ReservationCreateDto dto, ClaimsPrincipal user);
        List<ReservationDetailsDto> GetReservationDetails(int id);
        void CancelReservation(string reservationNumber);
        void UpdateSeats(ReservationModifyDto dto, int userid);
    }
}
