using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Entities;
using TicketReservationSystem.Exceptions;
using TicketReservationSystem.Services.Interfaces;
using TicketReservationSystem.Entities.Enums;
using TicketReservationSystem.DTO.SeatDto;
using TicketReservationSystem.DTO.ReservationDto;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TicketReservationSystem.Services
{
    public class ReservationService: IReservationService
    {
        private readonly TicketSystemDbContext _context;
        public ReservationService(TicketSystemDbContext context)
        {
            _context = context;
        }

        public void CancelReservation(string reservationNumber)
        {
            var reservation = _context.Reservations
                .Include(r => r.Seats)
                .Include(r => r.Payment)
                .FirstOrDefault(r => r.ReservationNumber == reservationNumber);

            if(reservation == null)
            {
                throw new NotFoundException("Reservation with specified id doesn't exist");
            }

            if(reservation.Status == ReservationStatus.Cancelled || reservation.Status == ReservationStatus.Expired)
            {
                throw new BadRequestException("Reservation is already cancelled or expired");
            }

            foreach(var seat in reservation.Seats)
            {
                seat.IsReserved = false;
            }

            reservation.Status = ReservationStatus.Cancelled;

            if(reservation.Payment != null && reservation.Payment.PaymentStatus == PaymentStatus.Completed)
            {
                reservation.Payment.PaymentStatus = PaymentStatus.Refunded;
            }

            _context.SaveChanges();
        }

        public void CreateReservation(ReservationCreateDto dto, ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            if(!int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID in token");
            }

            var userEntity = _context.Users
                .Include(u => u.Customer) // Ładowanie relacji z Customer
                .FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                throw new NotFoundException("User not found.");
            }

            if (userEntity.Customer == null)
            {
                throw new NotFoundException("No customer profile associated with the user.");
            }

            var eventEntity = _context.Events
                .Include(e => e.Seats)
                .FirstOrDefault(e => e.Name == dto.EventName && e.EventDate == dto.EventDate);

            if (eventEntity == null)
            {
                throw new NotFoundException("The event with the specified name and date was not found.");
            }

            var selectedSeats = eventEntity.Seats
                .Where(s => dto.Seats.Any(sel => sel.Row == s.Row && sel.SeatNumber == s.SeatNumber))
                .ToList();

            if(selectedSeats.Count != dto.Seats.Count)
            {
                throw new BadRequestException("Some of the selected seats are inaccessible.");
            }

            if(selectedSeats.Any(s => s.IsReserved))
            {
                throw new BadRequestException("Some of the selected seats are already reserved");
            }

            var reservationNumber = GenerateReservationNumber();

            var reservation = new Reservation
            {
                CustomerId = userEntity.Customer.Id,
                EventId = eventEntity.Id,
                ReservationNumber = reservationNumber,
                Seats = selectedSeats,
                CreatedAt = DateTime.Now.Date,
                Status = ReservationStatus.Pending
            };

            foreach(var seat in selectedSeats )
            {
                seat.IsReserved = true;
            }

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

        }

        public List<ReservationDetailsDto> GetReservationDetails(int userid)
        {
            var reservations = _context.Reservations
                .Where(r => r.CustomerId == userid)
                .Include(r => r.Event)
                .Include(r => r.Seats)
                .ToList();

            var reservationsDto = reservations.Select(r => new ReservationDetailsDto
            {
                ReservationNumber = r.ReservationNumber,
                EventName = r.Event.Name,
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                Seats = r.Seats.Select(s => new SeatSelectionDto
                {
                    SeatNumber = s.SeatNumber,
                    Row = s.Row
                }).ToList()
            }).ToList();

            return reservationsDto;
        }

        public void UpdateSeats(ReservationModifyDto dto, int userid)
        {
            var reservation = _context.Reservations
                .Include(r => r.Seats)
                .Include(r => r.Event)
                .ThenInclude(e => e.Seats)
                .FirstOrDefault(r => r.ReservationNumber == dto.ReservationNumber && r.Customer.UserId == userid);

            if(reservation == null)
            {
                throw new NotFoundException("Reservation not found or does not belong to the current user.");
            }

            var availableSeats = reservation.Event.Seats
                .Where(s => dto.AddSeats.Any(sel => sel.Row == s.Row && sel.SeatNumber == s.SeatNumber))
                .ToList();

            if(availableSeats.Count != dto.AddSeats.Count)
            {
                throw new BadRequestException("Some of the requested seats to add are not available.");
            }

            if (availableSeats.Any(s => s.IsReserved))
            {
                throw new BadRequestException("Some of the requested seats to add are already reserved.");
            }

            foreach (var seat in availableSeats)
            {
                seat.IsReserved = true;
                reservation.Seats.Add(seat);
            }

            var seatsToRemove = reservation.Seats
                .Where(s => dto.RemoveSeats.Any(sel => sel.Row == s.Row && sel.SeatNumber == s.SeatNumber))
                .ToList();

            if (seatsToRemove.Count != dto.RemoveSeats.Count)
            {
                throw new BadRequestException("Some of the requested seats to remove do not belong to this reservation.");
            }

            foreach (var seat in seatsToRemove)
            {
                seat.IsReserved = false;
                reservation.Seats.Remove(seat);
            }

            _context.SaveChanges();
        }

        private string GenerateReservationNumber()
        {
            var now = DateTime.Now;
            var guidPart = Guid.NewGuid().ToString("N").Substring(0,5);
            return $"RES/{now:yyyy}/{now:MM}/{now:dd}/{guidPart}";
        }


    }
}
