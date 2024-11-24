using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.DTO;
using TicketReservationSystem.Entities;
using TicketReservationSystem.Exceptions;
using TicketReservationSystem.Services.Interfaces;

namespace TicketReservationSystem.Services
{
    public class EventService : IEventService
    {
        private readonly TicketSystemDbContext _context;
        private readonly IMapper _mapper;
        public EventService(TicketSystemDbContext ticketSystemDbContext, IMapper mapper)
        {
            _context = ticketSystemDbContext;
            _mapper = mapper;
        }
        public void CreateEventWithGeneratedSeats(EventCreateDto eventDto)
        {

            if (eventDto.Rows <= 0 || eventDto.TotalSeats <= 0)
            {
                throw new BadRequestException("Wiersze i/lub ilosc miejsc musza byc wieksze niz 0");
            }

            var newEvent = _mapper.Map<Event>(eventDto);

            if (eventDto.TotalSeats % eventDto.Rows == 0)
            {
                int seatsPerRow = eventDto.TotalSeats / eventDto.Rows;
                for (int row = 1; row <= eventDto.Rows; row++)
                {
                    for (int number = 1; number <= seatsPerRow; number++)
                    {
                        newEvent.Seats.Add(new Seat
                        {
                            Row = row,
                            SeatNumber = number,
                            IsReserved = false
                        });
                    }
                }
            }
            else
            {
                newEvent.Rows = newEvent.Rows + 1;
                int rest = eventDto.TotalSeats % eventDto.Rows;
                int seatsPerRow = eventDto.TotalSeats / eventDto.Rows;
                for (int row = 1; row <= eventDto.Rows; row++)
                {
                    for (int number = 1; number <= seatsPerRow; number++)
                    {
                        newEvent.Seats.Add(new Seat
                        {
                            Row = row,
                            SeatNumber = number,
                            IsReserved = false
                        });
                    }
                    if (row == eventDto.Rows)
                    {
                        row = newEvent.Rows;
                        for (int number = 1; number <= rest; number++)
                        {
                            newEvent.Seats.Add(new Seat
                            {
                                Row = row,
                                SeatNumber = number,
                                IsReserved = false
                            });
                        }
                    }
                }
            }

            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var eventName =_context.Events.FirstOrDefault(e => e.Id == id);
            if(eventName == null)
            {
                throw new NotFoundException("This event doesn't exist");
            }

            _context.Events.Remove(eventName);
            _context.SaveChanges();
        }

        public EventDetailsDTO GetEventDetails(int id)
        {
            var eventDto = _context
                .Events
                .Include(e => e.Seats)
                .FirstOrDefault(e => e.Id == id);

            if(eventDto == null)
            {
                throw new NotFoundException("Nie znaleziono eventu o podanym id");
            }

            var result = _mapper.Map<EventDetailsDTO>(eventDto);
            result.AvailableSeats = eventDto.Seats.Count(s => !s.IsReserved);
            return result;
        }

        public ICollection<SeatDTO> GetSeats(int id)
        {
            var ev = _context.Events
                .Include(e => e.Seats)
                .FirstOrDefault(e => e.Id == id);

            if (ev == null)
            {
                throw new NotFoundException("Nie mozna odznalezc szczegółów na temat miejsc dla eventu, który nie istnieje");
            }

            var seatDetails = _mapper.Map<List<SeatDTO>>(ev.Seats);
            return seatDetails;

        }

        public void ModifyEvent(int id, EventCreateDto dto)
        {
            var eventName = _context.Events.FirstOrDefault(e => e.Id == id);
            if(eventName == null)
            {
                throw new NotFoundException("This event doesn't exist");
            }

            _mapper.Map(dto, eventName);

            _context.Events.Update(eventName);
            _context.SaveChanges();
        }
    }
}
