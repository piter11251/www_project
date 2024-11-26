using TicketReservationSystem.DTO.PaymentDto;
using TicketReservationSystem.Entities.Enums;

namespace TicketReservationSystem.Services.Interfaces
{
    public interface IPaymentService
    {
        void InitiatePayment(PaymentDto dto);
        void CompletePayment(PaymentDto dto);
    }
}
