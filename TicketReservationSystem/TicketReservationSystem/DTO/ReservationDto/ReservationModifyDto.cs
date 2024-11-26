using TicketReservationSystem.DTO.SeatDto;

namespace TicketReservationSystem.DTO.ReservationDto
{
    public class ReservationModifyDto
    {
        public string ReservationNumber { get; set; }
        public List<SeatSelectionDto> AddSeats { get; set; }
        public List<SeatSelectionDto> RemoveSeats { get; set; }
    }
}
