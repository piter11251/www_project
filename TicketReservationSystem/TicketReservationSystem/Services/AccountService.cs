using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketReservationSystem.DTO.AccountDto;
using TicketReservationSystem.DTO.CustomerDto;
using TicketReservationSystem.Entities;
using TicketReservationSystem.Exceptions;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Services
{
    public class AccountService: IAccountService
    {
        private readonly TicketSystemDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;
        public AccountService(TicketSystemDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
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

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials : credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void CompleteData(CustomerDataDto dto, ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Cannot find user identifier in token");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new ArgumentException("User identifier in token is invalid");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.UserId == userId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Cannot find customer info");
            }

            _mapper.Map(dto, customer);

            _context.Customers.Update(customer);
            _context.SaveChanges();

        }
    }
}
