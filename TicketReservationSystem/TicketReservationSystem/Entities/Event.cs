using System.ComponentModel.DataAnnotations;

namespace TicketReservationSystem.Entities
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }
        [Required]
        public int TotalSeats { get; set; }
        [Required]
        public int Rows { get; set; }
        public bool AgeRestrictions { get; set; }
        public DateTime EventDate { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public int AvailableSeats => Seats.Count(s => !s.IsReserved);
    }
}
