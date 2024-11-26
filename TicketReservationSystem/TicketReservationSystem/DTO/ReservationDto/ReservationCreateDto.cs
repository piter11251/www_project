using TicketReservationSystem.DTO.SeatDto;

namespace TicketReservationSystem.DTO.ReservationDto
{
    public class ReservationCreateDto
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public List<SeatSelectionDto> Seats { get; set; } = new List<SeatSelectionDto>();
    }
}
