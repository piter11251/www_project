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
        public EventService(TicketSystemDbContext ticketSystemDbContext)
        {
            _context = ticketSystemDbContext;
        }
        public void CreateEventWithGeneratedSeats(CreateEventDto eventDto)
        {

            if (eventDto.Rows <= 0 || eventDto.TotalSeats <= 0)
            {
                throw new BadRequestException("Wiersze i/lub ilosc miejsc musza byc wieksze niz 0");
            }

            var newEvent = new Event
            {
                Name = eventDto.Name,
                Description = eventDto.Description,
                TicketPrice = eventDto.TicketPrice,
                TotalSeats = eventDto.TotalSeats,
                Rows = eventDto.Rows
            };

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

            var result = new EventDetailsDTO
            {
                Name = eventDto.Name,
                Description = eventDto.Description,
                TicketPrice = eventDto.TicketPrice,
                AvailableSeats = eventDto.Seats.Count(s => !s.IsReserved)
            };
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

            var seatDetails = ev.Seats.Select(s => new SeatDTO
            {
                Row = s.Row,
                SeatNumber = s.SeatNumber,
                IsReserved = s.IsReserved,
            }).ToList();

            return seatDetails;
        }
    }
}
