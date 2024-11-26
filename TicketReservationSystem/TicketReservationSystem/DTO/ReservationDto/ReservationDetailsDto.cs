using TicketReservationSystem.DTO.SeatDto;
using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.DTO.ReservationDto
{
    public class ReservationDetailsDto
    {
        public string ReservationNumber { get; set; }
        public string EventName { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<SeatSelectionDto> Seats { get; set; }
    }
}
