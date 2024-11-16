namespace TicketReservationSystem.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal TicketPrice { get; set; }
        public int TotalSeats { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public int AvailableSeats => Seats.Count(s => !s.IsReserved);
    }
}
