using FluentValidation;
using TicketReservationSystem.DTO.EventDto;

namespace TicketReservationSystem.DTO.Validators
{
    public class EventCreateDtoValidator: AbstractValidator<EventCreateDto>
    {
        public EventCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Enter event name");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Event name doesn't exist");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Enter some event description");

            RuleFor(x => x.TicketPrice)
                .NotEmpty()
                .WithMessage("Enter ticket price");

            RuleFor(x => x.TicketPrice)
                .NotNull()
                .WithMessage("Enter ticket price");

            RuleFor(x => x.TicketPrice)
                .PrecisionScale(7, 2, true)
                .WithMessage("It's too much money you god damn capitalist");
        }
    }
}
