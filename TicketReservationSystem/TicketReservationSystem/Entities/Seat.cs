namespace TicketReservationSystem.Entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int EventId {  get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public bool IsReserved { get; set; }
        public Reservation? Reservation { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}
