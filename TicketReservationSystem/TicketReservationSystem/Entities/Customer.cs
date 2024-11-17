namespace TicketReservationSystem.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.User;
        public DateTime? DateOfBirth { get; set; }
        // klient moze zarezerwować wiele miejsc na jednej rezerwacji
        public Reservation? Reservation { get; set; }
    }
}
