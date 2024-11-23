using Microsoft.AspNetCore.Identity;
using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.Entities
{
    public class User: IdentityUser
    {
        public UserRole Role { get; set; } = UserRole.User;
        public Customer Customer { get; set; }
    }
}
