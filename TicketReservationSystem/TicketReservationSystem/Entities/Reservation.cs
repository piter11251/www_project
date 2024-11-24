using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public string ReservationNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReservationStatus Status {  get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public Event Event { get; set; } = null!;
        public Payment? Payment { get; set; }
    }
}
