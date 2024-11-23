namespace TicketReservationSystem.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        // klient moze zarezerwować wiele miejsc na jednej rezerwacji
        public Reservation? Reservation { get; set; }
        public User User { get; set; }
    }
}
