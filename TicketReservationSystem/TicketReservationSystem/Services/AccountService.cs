using Microsoft.AspNetCore.Identity;
using TicketReservationSystem.DTO;
using TicketReservationSystem.Entities;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Services
{
    public class AccountService: IAccountService
    {
        private readonly TicketSystemDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
    
        public AccountService(TicketSystemDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.HashedPassword = hashedPassword;

            var customer = new Customer()
            {
                User = newUser
            };
            newUser.Customer = customer;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
