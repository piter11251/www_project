namespace TicketReservationSystem.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime PaymentDate {  get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public Reservation Reservation = null!;
    }
}
