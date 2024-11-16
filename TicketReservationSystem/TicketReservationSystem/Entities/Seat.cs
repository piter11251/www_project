namespace TicketReservationSystem.Entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int EventId {  get; set; }
        public string Row { get; set; } = null!;
        public int SeatNumber { get; set; }
        public bool IsReserved { get; set; }
        public Reservation? Reservation { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}
