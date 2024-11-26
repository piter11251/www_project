namespace TicketReservationSystem.DTO.EventDto
{
    public class EventDetailsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TicketPrice { get; set; }
        public int AvailableSeats { get; set; }
    }
}
