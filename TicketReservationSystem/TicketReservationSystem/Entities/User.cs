using Microsoft.AspNetCore.Identity;
using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public Customer Customer { get; set; }
    }
}
