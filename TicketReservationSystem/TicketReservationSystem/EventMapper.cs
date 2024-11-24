using AutoMapper;
using TicketReservationSystem.DTO;
using TicketReservationSystem.Entities;

namespace TicketReservationSystem
{
    public class EventMapper: Profile
    {
        public EventMapper()
        {
            CreateMap<EventModifyDto, Event>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EventCreateDto, Event>();

            CreateMap<Event, EventDetailsDTO>()
                .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore());

            CreateMap<Seat, SeatDTO>();

            CreateMap<CustomerDataDto, Customer>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CustomerDataDto, Customer>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Date));
        }
    }
}
