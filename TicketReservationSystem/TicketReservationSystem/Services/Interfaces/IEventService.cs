﻿using TicketReservationSystem.DTO;

namespace TicketReservationSystem.Services.Interfaces
{
    public interface IEventService
    {
        void CreateEventWithGeneratedSeats(EventCreateDto eventDto);
        EventDetailsDTO GetEventDetails(int id);
        ICollection<SeatDTO> GetSeats(int id);
        void DeleteEvent(int id);
        void ModifyEvent(int id, EventCreateDto dto);
    }
}
