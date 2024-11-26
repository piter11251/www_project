using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.DTO.PaymentDto;
using TicketReservationSystem.Entities;
using TicketReservationSystem.Entities.Enums;
using TicketReservationSystem.Exceptions;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Services
{
   
    public class PaymentService: IPaymentService
    {
        private readonly TicketSystemDbContext _context;
        public PaymentService(TicketSystemDbContext context)
        {
            _context = context;
        }

       

        public void InitiatePayment(PaymentDto dto)
        {
            var reservation = _context.Reservations
                .Include(r => r.Payment)
                .FirstOrDefault(r => r.ReservationNumber == dto.ReservationNumber);

            if (reservation == null)
            {
                throw new NotFoundException("Not found reservation with specified number");
            }
            if(reservation.Payment != null)
            {
                throw new BadRequestException("You have already paid for this reservation");
            }

            var payment = new Payment
            {
                ReservationId = reservation.Id,
                PaymentDate = DateTime.Now,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = PaymentStatus.Pending
            };

            reservation.Status = ReservationStatus.Pending;

            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void CompletePayment(PaymentDto dto)
        {
            var payment = _context.Payments
                .Include(p => p.Reservation)
                .FirstOrDefault(p => p.Reservation.ReservationNumber == dto.ReservationNumber);

            if(payment == null)
            {
                throw new NotFoundException("No payment found for the specified reservation number.");
            }

            if(payment.PaymentStatus == PaymentStatus.Completed)
            {
                throw new BadRequestException("This payment has already been processed.");
            }

            payment.PaymentStatus = PaymentStatus.Completed;
            payment.Reservation.Status = ReservationStatus.Confirmed;

            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        
    }
}
