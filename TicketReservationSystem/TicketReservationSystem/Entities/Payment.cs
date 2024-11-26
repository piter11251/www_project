using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime PaymentDate {  get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Reservation Reservation { get; set; } = null!;
    }
}
