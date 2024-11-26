namespace TicketReservationSystem.DTO.EventDto
{
    public class EventCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TicketPrice { get; set; }
        public int Rows { get; set; }
        public int TotalSeats { get; set; }
        public bool AgeRestrictions { get; set; }
    }
}
