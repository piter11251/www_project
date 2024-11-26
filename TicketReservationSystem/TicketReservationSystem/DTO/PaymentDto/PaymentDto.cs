using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.DTO.PaymentDto
{
    public class PaymentDto
    {
        public string ReservationNumber { get; set; } 
        public PaymentMethod PaymentMethod { get; set; }
    }
}
