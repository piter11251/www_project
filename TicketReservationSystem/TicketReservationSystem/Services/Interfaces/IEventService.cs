using TicketReservationSystem.DTO;

namespace TicketReservationSystem.Services.Interfaces
{
    public interface IEventService
    {
        void CreateEventWithGeneratedSeats(CreateEventDto eventDto);
        EventDetailsDTO GetEventDetails(int id);
        ICollection<SeatDTO> GetSeats(int id);
    }
}
