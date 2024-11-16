namespace TicketReservationSystem.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public Event Event { get; set; } = null!;
        public Payment? Payment { get; set; }
    }
}
